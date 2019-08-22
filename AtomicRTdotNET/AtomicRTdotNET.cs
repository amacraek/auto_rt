using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.TypeInspectors;

// TODO: AtomicRTdotNET.cs (1)
// enum for the executor names, so only known executors are accepted

public class AtomicRTdotNET
{
    #region Fields and properties
    private string atomicsPath;
    public Dictionary<string, string> Techniques { get; private set; } = new Dictionary<string, string>();

    /// <summary>
    /// The path to the "atomics" subfolder of the Atomic RT repo.
    /// </summary>
    public string AtomicsPath
    {
        // the public AtomicsPath property accesses the private atomicsPath field, as to control how the property is assigned a value.
        get
        {
            return atomicsPath;
        }
        set
        {
            // if you do this:
            //      AtomicRTdotNET.AtomicsPath = @"..\atomicRT\atomics";
            // the value of atomicsPath will include a backslash (i.e. @"..\atomicRT\atomics\")
            atomicsPath = PathAddBackslash(value);
        }
    }

    #endregion

    #region Constructors
    /// <summary>
    /// Empty constructor for AtomicRTdotNET. The path to the Atomic RT 'atomics' folder must be set manually.
    /// </summary>
    public AtomicRTdotNET()
    {
        atomicsPath = null;
    }

    /// <summary>
    /// Constructor for AtomicRTdotNET that sets the path to the Atomic RT 'atomics' folder, which is required for some methods.
    /// </summary>
    /// <param name="atomicsPath">The full directory to the Atomic Red Team 'atomics' folder.</param>
    public AtomicRTdotNET(string atomicsPath)
    {
        atomicsPath = PathAddBackslash(atomicsPath);
    }

    #endregion
    
    #region "Atomic" data structure
    /// <summary>
    /// Organizes the data stored in one atomic technique, e.g. all data in "T1017.yaml".
    /// </summary>
    public struct Atomic
    {
        public string AttackTechnique { get; set; }
        public string DisplayName { get; set; }
        public List<AtomicTest> AtomicTests { get; set; }
    }

    /// <summary>
    /// Stores information about an individual atomic test, including its arguments and its executor.
    /// </summary>
    public struct AtomicTest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> SupportedPlatforms { get; set; }
        public Dictionary<string, InputArgument> InputArguments { get; set; }
        public Executor Executor { get; set; }

    }

    /// <summary>
    /// Stores information about an individual input argument of a test. 
    /// </summary>
    public struct InputArgument
    {
        public string Description { get; set; }
        public string Type { get; set; }
        [YamlMember(Alias = "default", ApplyNamingConventions = false)]
        public string Value { get; set; }
    }

    /// <summary>
    /// Stores information about the executor/command associated with one test.
    /// </summary>
    public struct Executor
    {
        public string Name { get; set; }
        public string Command { get; set; }
    }
    #endregion

    #region YAML serializing and deserializing 

    /// <summary>
    /// Given a full file path, deserialize a "TXXXX.yaml" file into an <see cref="Atomic"/>.
    /// </summary>
    /// <remarks>
    /// Use <see cref="GetAtomicByTechnique(string)"/> to create an <see cref="Atomic"/> from the technique code, e.g. from "T1017".
    /// </remarks>
    /// <param name="filePath">The full file path to the YAML file.</param>
    /// <returns>An <see cref="Atomic"/> containing the deserialized YAML.</returns>
    public Atomic GetAtomicByYAMLPath(string filePath)
    {
        var input = new StreamReader(filePath);
        var deserializer = new DeserializerBuilder().WithNamingConvention(new UnderscoredNamingConvention()).Build();
        Atomic output = deserializer.Deserialize<Atomic>(input);
        input.Close();
        return output;
    }

    /// <summary>
    /// Given a technique code "TXXXX", deserialize its corresponding "TXXXX.yaml" file into an <see cref="Atomic"/>.
    /// </summary>
    /// <remarks>
    /// Use <see cref="GetAtomicByYAMLPath(string)"/> to create an <see cref="Atomic"/> from a full file path.
    /// </remarks>
    /// <param name="filePath">The technique code, e.g. "T1017".</param>
    /// <returns>An <see cref="Atomic"/> containing the deserialized YAML that corresponds to the technique.</returns>
    public Atomic GetAtomicByTechnique(string attackTechnique)
    {
        if (atomicsPath == null)
        {
            throw new InvalidOperationException("Cannot open attack by technique name because the atomics directory 'AtomicsPath' is null.");
        }
        string filePath = string.Join("", atomicsPath, attackTechnique, @"\", attackTechnique, ".yaml");
        var input = new StreamReader(filePath);
        var deserializer = new DeserializerBuilder().WithNamingConvention(new UnderscoredNamingConvention()).IgnoreUnmatchedProperties().Build();
        Atomic output = deserializer.Deserialize<Atomic>(input);
        input.Close();
        return output;
    }    
     
    /// <summary>
    /// Serializes an <see cref="Atomic"/> into YAML format, then writes the data to the specified file.
    /// </summary>
    /// <param name="outpath">The path to save the serialized <see cref="Atomic"/></param>
    /// <param name="atomic">The <see cref="Atomic"/> to serialize.</param>
    public void SaveAtomicAsYAML(string outpath, Atomic atomic)
    {
        var builder = new SerializerBuilder().WithNamingConvention(new UnderscoredNamingConvention()).DisableAliases();
        var serializer = builder.WithTypeInspector(inspector => new IgnoreNullTypeInspector(inspector)).Build();
        StreamWriter streamWriter = new StreamWriter(outpath);
        serializer.Serialize(streamWriter, atomic);
        streamWriter.Flush();
        streamWriter.Close();
    }

    #endregion

    #region TreeView nodes < = > Atomic

    /// <summary>
    /// Convert a <see cref="TreeNode[]"/> to an <see cref="Atomic"/>. <para/>
    /// This is the converse of <see cref="AtomicToTreeNodes(Atomic)"/>.
    /// </summary>
    /// <param name="treeNodes">The <see cref="TreeNode[]"/> containing info about the technique.</param>
    /// <param name="technique">The technique code, e.g. "T1017".</param>
    /// <param name="name">The display name of the technique, e.g. "Mimikatz via Powershell".</param>
    /// <returns>An <see cref="Atomic"/> containing the given information.</returns>
    public Atomic TreeNodesToAtomic(TreeNode[] treeNodes, string technique, string name)
    {
        Atomic outAtomic = new Atomic() { AttackTechnique = technique, DisplayName = name };
        List<AtomicTest> testsFromTree = new List<AtomicTest>();

        foreach (TreeNode testNode in treeNodes)
        {
            // because the way we serialize this (see next method), each node of the input represents a single atomic test
            AtomicTest thisTest = new AtomicTest
            {
                Name = testNode.Text
            };            

            // description
            thisTest.Description = testNode.Nodes["d"].FirstNode.Text;

            // platforms
            List<string> platforms = new List<string>();
            foreach (TreeNode node in testNode.Nodes["p"].Nodes)
            {
                platforms.Add(node.Text);
            }
            thisTest.SupportedPlatforms = platforms;

            // arguments 
            var arguments = new Dictionary<string, InputArgument>();
            if (testNode.Nodes["arg"] != null)
            {
                foreach (TreeNode node in testNode.Nodes["arg"].Nodes)
                {
                    InputArgument argument = new InputArgument
                    {
                        Description = node.Nodes["arg_d"].FirstNode.Text,
                        Type = node.Nodes["arg_t"].FirstNode.Text,
                        Value = node.Nodes["arg_v"].FirstNode.Text
                    };
                    arguments.Add(node.Text, argument);
                }
                thisTest.InputArguments = arguments;
            }

            // executor
            Executor executor = new Executor()
            {
                Name = testNode.Nodes["exec"].Nodes["exec_n"].FirstNode.Text,
                Command = testNode.Nodes["exec"].Nodes["exec_c"].FirstNode.Text
            };
            thisTest.Executor = executor;
            testsFromTree.Add(thisTest);
        }
        outAtomic.AtomicTests = testsFromTree;
        return outAtomic;
    }


    /// <summary>
    /// Convert an <see cref="Atomic"/> to a <see cref="TreeNode[]"/>.<para/>
    /// This is the converse of <see cref="TreeNodesToAtomic(TreeNode[], string, string)"/>.
    /// </summary>
    /// <param name="inAtomic">The <see cref="Atomic"/> to convert.</param>
    /// <returns>A <see cref="TreeNode[]"/> containing the given information.</returns>
    public TreeNode[] AtomicToTreeNodes(Atomic inAtomic)
    {
        List<TreeNode> outNode = new List<TreeNode>();

        if (inAtomic.AtomicTests != null)
        {
            foreach (AtomicTest test in inAtomic.AtomicTests)
            {
                // each individual test will represent one child node in the output TreeNode[]
                TreeNode testNode = new TreeNode(test.Name);

                // description
                TreeNode description = new TreeNode("Description") { Name = "d" };
                description.Nodes.Add("d_txt", test.Description);
                testNode.Nodes.Add(description);

                // supported platforms
                TreeNode platforms = new TreeNode("Supported Platforms") { Name = "p" };
                foreach (string platform in test.SupportedPlatforms)
                {
                    platforms.Nodes.Add(platform);
                }
                testNode.Nodes.Add(platforms);

                // input arguments
                if (test.InputArguments != null)
                {
                    TreeNode inputArgs = new TreeNode("Input Arguments") { Name = "arg" };
                    foreach (KeyValuePair<string, InputArgument> valuePair in test.InputArguments)
                    {
                        TreeNode argNode = new TreeNode(valuePair.Key);
                        argNode.Nodes.Add(new TreeNode("Description", new TreeNode[1] { new TreeNode(valuePair.Value.Description) }) { Name = "arg_d" });
                        argNode.Nodes.Add(new TreeNode("Type", new TreeNode[1] { new TreeNode(valuePair.Value.Type) }) { Name = "arg_t" });
                        argNode.Nodes.Add(new TreeNode("Default Value", new TreeNode[1] { new TreeNode(valuePair.Value.Value) }) { Name = "arg_v" });
                        inputArgs.Nodes.Add(argNode);
                    }
                    testNode.Nodes.Add(inputArgs);
                }

                // executor
                TreeNode executor = new TreeNode("Executor") { Name = "exec" };
                executor.Nodes.Add(new TreeNode("Name", new TreeNode[1] { new TreeNode(test.Executor.Name) }) { Name = "exec_n" });
                executor.Nodes.Add(new TreeNode("Command", new TreeNode[1] { new TreeNode(test.Executor.Command) }) { Name = "exec_c" });
                testNode.Nodes.Add(executor);

                // add to output
                outNode.Add(testNode);
            }
        }
        return outNode.ToArray();
    }

    #endregion

    #region Windows-specific methods

    /// <summary>
    /// Try to pull all valid Windows tests from the 'atomicRT\atomics\windows-index.md' file. <para/>
    /// You must set the value of <see cref="AtomicsPath"/> to make this work.
    /// </summary>
    /// <returns>A <see cref="Dictionary{string, string}"/> with { key = TechniqueCode; value = Description } if successful.<para/>
    /// <see cref="null"/> if unsuccessful.</returns>
    public Dictionary<string, string> ParseWindowsIndex()
    {
        try
        {
            StreamReader streamReader = new StreamReader(atomicsPath + @"\windows-index.md");        
            while (!streamReader.EndOfStream)
            {
                string thisLine = streamReader.ReadLine();
                if (thisLine.Contains("[CONTRIBUTE A TEST]") || thisLine.Contains("- Atomic Test #"))
                {
                    continue;
                }
                else
                {
                    Regex regexObj = new Regex(@"\[(T[0-9]{4})\s(.+?)\]");
                    MatchCollection allMatchResults = regexObj.Matches(thisLine);
                    if (allMatchResults.Count > 0)
                    {
                        string TechniqueCode = allMatchResults[0].Groups[1].Value;
                        string Description = allMatchResults[0].Groups[2].Value;
                        if (!Techniques.ContainsKey(TechniqueCode))
                        {
                            Techniques.Add(TechniqueCode, Description);
                        }
                    }
                }
            }
            streamReader.Close();
            return Techniques;
        }
        catch (Exception e)
        {
            MessageBox.Show("Error loading atomics: \r\n\r\n" + e.ToString());
            return null;
        }
    }

    /// <summary>
    /// Build the Windows-specific executors, i.e. the commands associated with a test, from an <see cref="Atomic"/>.
    /// </summary>
    /// <param name="validAtomic"></param>
    /// <returns></returns>
    public List<string> BuildWindowsExecutors(Atomic validAtomic)
    {
        List<string> output = new List<string>();
        foreach (AtomicTest atomicTest in validAtomic.AtomicTests)
        {
            if (atomicTest.SupportedPlatforms.Contains("windows"))
            {
                string command = atomicTest.Executor.Command;
                try
                {
                    Regex argMarkup = new Regex(@"(#\{.+?\})");
                    Match matchResult = argMarkup.Match(command);
                    while (matchResult.Success)
                    {
                        string thisMatch = matchResult.Value;
                        command = command.Replace(thisMatch, atomicTest.InputArguments[thisMatch.Substring(2, thisMatch.Length - 3)].Value);
                        matchResult = matchResult.NextMatch();
                    }
                }
                catch
                {
                    // do nothing
                }
                switch (atomicTest.Executor.Name)
                {
                    case "powershell":
                        output.Add(string.Join("", "powershell ", command));
                        break;

                    case "command_prompt":
                        output.Add(command);
                        break;

                    default:
                        break;
                }
            }
        }
        return output;
    }
    #endregion

    #region Miscellaneous
    /// <summary>
    /// Get the path to the .yaml file corresponding to a technique. <para/>
    /// E.g. given <paramref name="technique"/> "T1017", <see cref="GetYAMLPathFromTechnique(string)"/> returns the full filepath to "T1017.yaml".
    /// </summary>
    /// <param name="technique"></param>
    /// <returns></returns>
    public string GetYAMLPathFromTechnique(string technique)
    {
        return string.Join("", atomicsPath, technique, @"\", technique, ".yaml");
    }

    private string PathAddBackslash(string path)
    {
        // stack overflow 20405965
        string separator1 = Path.DirectorySeparatorChar.ToString();
        string separator2 = Path.AltDirectorySeparatorChar.ToString();
        path = path.TrimEnd();
        if (path.EndsWith(separator1) || path.EndsWith(separator2))
            return path;
        if (path.Contains(separator2))
            return path + separator2;
        return path + separator1;
    }
    #endregion

}

/// <summary>
/// Type inspector for serializing Atomics into yaml using <see cref="YamlDotNet"/>.
/// Found this somewhere on StackOverflow but I forgot where.
/// </summary>
public class IgnoreNullTypeInspector : TypeInspectorSkeleton
{    
    private readonly ITypeInspector _innerTypeDescriptor;

    public IgnoreNullTypeInspector(ITypeInspector innerTypeDescriptor)
    {
        _innerTypeDescriptor = innerTypeDescriptor;
    }

    public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
    {        
        var props = _innerTypeDescriptor.GetProperties(type, container);
        // ignore properties that are null when serializing
        props = props.Where(p => !(p.Type == null));
        return props;
    }

}


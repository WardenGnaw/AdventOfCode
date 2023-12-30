using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Problems
{
    public interface FileSystemObject
    {
        string Name { get; }
        long Size { get; }
    }

    public class FileNode : FileSystemObject
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }

    public class Folder : FileSystemObject
    {
        public string Name { get; set; }
        public Dictionary<string, FileSystemObject> Contents { get; set; }
        public Folder ParentFolder { get; set; }
        public long Size
        {
            get
            {
                long size = 0;
                foreach (var item in Contents.Values)
                {
                    size += item.Size;
                }
                return size;
            }
        }

        public void AddFile(string name, long size)
        {
            this.Contents[name] = new FileNode()
            {
                Name = name,
                Size = size
            };
        }

        public void AddFolder(string name)
        {
            this.Contents[name] = new Folder()
            {
                Name = name,
                Contents = new Dictionary<string, FileSystemObject>(),
                ParentFolder = this
            };
        }

    }

    public class FileSystem
    {
        public Folder CurrentFolder { get; set; }
        public Folder RootFolder { get; set; }
    }

    [TestClass]
    public class Day7
    {
        [TestMethod]
        public void Part1()
        {
            FileSystem fs = new FileSystem();
            fs.RootFolder = new Folder()
            {
                Name = "/",
                Contents = new Dictionary<string, FileSystemObject>(),
                ParentFolder = null
            };

            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day7.txt")).Trim().Split("\r\n");
            for(int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                if (line.StartsWith("$"))
                {
                    line = line.Substring(2); // Remove '$ '
                    if (line.StartsWith("cd"))
                    {
                        line = line.Substring(3); // Remove 'cd '
                        if (line.Equals("/"))
                        {
                            fs.CurrentFolder = fs.RootFolder;
                        }
                        else if (line.Equals(".."))
                        {
                            fs.CurrentFolder = fs.CurrentFolder.ParentFolder;
                        }
                        else
                        {
                            fs.CurrentFolder = fs.CurrentFolder.Contents[line] as Folder;
                        }
                    }
                    else if (line.StartsWith("ls"))
                    {
                        while ((i + 1) < data.Length && !data[i + 1].StartsWith("$"))
                        {
                            string output = data[++i];
                            if (output.StartsWith("dir"))
                            {
                                output = output.Substring(4); // 'dir '
                                fs.CurrentFolder.AddFolder(output);
                            }
                            else
                            {
                                string[] fileData = output.Split(" ");
                                string name = fileData[1];
                                long size = long.Parse(fileData[0]);
                                fs.CurrentFolder.AddFile(name, size);
                            }
                        }
                    }
                }
            }
            long total = 0;
            Queue<FileSystemObject> q = new Queue<FileSystemObject>();
            q.Enqueue(fs.RootFolder);
            while (q.Any())
            {
                var fso = q.Dequeue();
                if (fso is Folder folder)
                {
                    long size = folder.Size;
                    if (size < 100_000)
                    {
                        total += size;
                    }
                    foreach (var c in folder.Contents.Values)
                    {
                        if (c is Folder)
                        {
                            q.Enqueue(c);
                        }
                    }
                }
            }
            
            Assert.Fail("" + total);
        }

        [TestMethod]
        public void Part2()
        {
            FileSystem fs = new FileSystem();
            fs.RootFolder = new Folder()
            {
                Name = "/",
                Contents = new Dictionary<string, FileSystemObject>(),
                ParentFolder = null
            };

            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day7.txt")).Trim().Split("\r\n");
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                if (line.StartsWith("$"))
                {
                    line = line.Substring(2); // Remove '$ '
                    if (line.StartsWith("cd"))
                    {
                        line = line.Substring(3); // Remove 'cd '
                        if (line.Equals("/"))
                        {
                            fs.CurrentFolder = fs.RootFolder;
                        }
                        else if (line.Equals(".."))
                        {
                            fs.CurrentFolder = fs.CurrentFolder.ParentFolder;
                        }
                        else
                        {
                            fs.CurrentFolder = fs.CurrentFolder.Contents[line] as Folder;
                        }
                    }
                    else if (line.StartsWith("ls"))
                    {
                        while ((i + 1) < data.Length && !data[i + 1].StartsWith("$"))
                        {
                            string output = data[++i];
                            if (output.StartsWith("dir"))
                            {
                                output = output.Substring(4); // 'dir '
                                fs.CurrentFolder.AddFolder(output);
                            }
                            else
                            {
                                string[] fileData = output.Split(" ");
                                string name = fileData[1];
                                long size = long.Parse(fileData[0]);
                                fs.CurrentFolder.AddFile(name, size);
                            }
                        }
                    }
                }
            }
            long spaceAvaliable = 70000000 - fs.RootFolder.Size;
            List<long> sizes = new List<long>();
            Queue<FileSystemObject> q = new Queue<FileSystemObject>();
            q.Enqueue(fs.RootFolder);
            while (q.Any())
            {
                var fso = q.Dequeue();
                if (fso is Folder folder)
                {
                    long size = folder.Size;
                    if ((spaceAvaliable + size) > 30_000_000)
                    {
                        sizes.Add(size);
                    }
                    foreach (var c in folder.Contents.Values)
                    {
                        if (c is Folder)
                        {
                            q.Enqueue(c);
                        }
                    }
                }
            }

            Assert.Fail("" + sizes.OrderBy(x => x).Take(1).Sum());
        }
    }
}
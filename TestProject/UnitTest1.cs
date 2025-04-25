using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var adminRole = new AdminRole();
            var userRole = new ViewRole();
            var adminUser = new User("Admin", adminRole);
            var regularUser = new User("Egor", userRole);


            adminUser.ManageUsers(regularUser, adminRole);


            Assert.IsType<AdminRole>(regularUser.Role);
        }

        private readonly User _adminUser = new User("admin", new AdminRole());
        private readonly User _viewerUser = new User("viewer", new ViewRole());

        [Fact]

        public void Test2()
        {


            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[] { "line1", "line2" });
            var manager = new DocumentManager(_adminUser);


            manager.OpenDocument(tempFile);


            Assert.Equal(new[] { "line1", "line2" }, manager.CurrentDocument);


            File.Delete(tempFile);

        }


        [Fact]

        public void Test3()
        {


            var manager = new DocumentManager(_adminUser);
            manager.CreateDocument(new List<string> { "test" });
            var tempFile = Path.GetTempFileName();
            File.Delete(tempFile); 


            manager.SaveDocument("JSON", tempFile);


            Assert.True(File.Exists(tempFile));
            var content = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(tempFile));
            Assert.Equal(new[] { "test" }, content);


            File.Delete(tempFile);

        }

        [Fact]

        public void Test4()
        {

            var manager = new DocumentManager(_adminUser);
            manager.CreateDocument(new List<string> { "original" });


            manager.EditDocument(new List<string> { "modified" });


            Assert.Equal(new[] { "modified" }, manager.CurrentDocument);

        }

        [Fact]

        public void Test5()
        {


            var manager = new DocumentManager(_viewerUser);
            manager._documentLines = new List<string> { "original" }; // Явно устанавливаем начальное содержимое


            manager.EditDocument(new List<string> { "modified" });


            Assert.Equal(new[] { "original" }, manager.CurrentDocument);

        }

        [Fact]

        public void Test6()
        {


            var tempFile = Path.GetTempFileName();
            var manager = new DocumentManager(_adminUser);

            manager.DeleteDocument(tempFile);


            Assert.False(File.Exists(tempFile));

        }

        [Fact]

        public void Test7()
        {

            var manager = new DocumentManager(_adminUser);
            manager.CreateDocument(new List<string> { "test content" });


            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            manager.ViewDocument();
            Assert.Contains("test content", consoleOutput.ToString());

        }

        [Fact]

        public void Test8()
        {

            var manager = new DocumentManager(_adminUser);
            manager.CreateDocument(new List<string> { "v1" });

            manager.EditDocument(new List<string> { "v2" });
            manager.EditDocument(new List<string> { "v3" });

            Assert.Equal(2, manager._history.Versions.Count);
            Assert.Contains("Редактирование документа",
                manager._history.Versions[0].Description);

        }

        [Fact]

        public void Test9()
        {

  
            var manager = new DocumentManager(_adminUser);
            var testLines = new List<string> { "<root>", "  <item>Test</item>", "</root>" };
            manager.CreateDocument(testLines);

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xml");

            try
            {
            
                manager.SaveDocument("XML", tempFile);

               
                Assert.True(File.Exists(tempFile));

                
                var newManager = new DocumentManager(_adminUser);
                newManager.OpenDocument(tempFile);

                
                Assert.Equal(testLines, newManager.CurrentDocument);
            }
            finally
            {
                
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]

        public void Test10()
        {

           
            var manager = new DocumentManager(_viewerUser); 
            manager.CreateDocument(new List<string> { "content" });
            var tempFile = Path.GetTempFileName();
            File.Delete(tempFile);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            
            manager.SaveDocument("JSON", tempFile);

           
            Assert.False(File.Exists(tempFile)); 
            Assert.Contains("нет прав", consoleOutput.ToString());


        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

/*
 *   Kinda Funny Soundboard app
 *   Data bundle build and content validation tool
 *   Validates soundboard data, and optionally builds a binary
 *   to be loaded directly into the app for testing.
 *   
 *   Created by Tom Taylor (tomxxi)
 *   April 2019
 *   
 *   Recent edits:
 *   DD   MM   YYYY
 *   12 / 04 / 2019 - Created this project and got it working. - TT
 * 
 *                                                                              */

namespace KFSB_Data_Builder
{
    class Program
    {
        const string OUTPUT_FILE = "data.xxi";

        const string DATA_PATH = @"..\data\";
        const string OUTPUT_PATH = @"..\output";
        const string FULL_OUTPUT = OUTPUT_PATH + @"\" + OUTPUT_FILE;
        const string LOADING_JSON_STR = "Validating the '{0}' file...";
        const string JSON_MISSING_STR = "The '{0}' file is not valid. Make sure that you're running this application in the 'tools' folder, and the soundboard data is in the 'data' folder.";

        static void Main(string[] args)
        {
            // Only validate by default.
            // Most people don't need to build.
            bool validateOnly = true;

            if (args.Length > 0)
            {
                // But build if requested via argument.
                if (args[0] == "build")
                {
                    validateOnly = false;
                }
            }

            Console.WriteLine("Kinda Funny Soundboard data");
            Console.WriteLine("Validation and build tool");
            Console.WriteLine("");
            Console.WriteLine("Starting validation...");
            Console.WriteLine("");

            if (ValidateData())
            {
                Console.WriteLine("");
                Console.WriteLine("Validation successful. The content appears to be correctly assembled.");
                Console.WriteLine("To submit this new content to be included in the app, make a pull request now. Your data will be tested and if it passes, your additions will make it into the app!");
                Console.WriteLine("");

                if (!validateOnly)
                {
                    #pragma warning disable CS0162 // Unreachable code detected
                    if (BuildDataBundle())
                    {
                        Console.WriteLine(string.Format("Build successful. The bundle is located at: {0}", Path.GetFullPath(FULL_OUTPUT)));
                        Console.WriteLine("To submit this build to be included in the app, make a pull request now. Your build will be tested and if it passes, your additions will make it into the app!");
                    } else
                    {
                        Console.WriteLine("Build failed. Please ensure that you have the correct permissions to write files.");
                    }
                    #pragma warning restore CS0162 // Unreachable code detected
                }

            } else
            {
                Console.WriteLine("");
                Console.WriteLine("Validation failed... Please check the error messages for more information.");
            }

            Console.WriteLine("Press enter to finish.");
            Console.ReadKey();
        }

        private static bool BuildDataBundle()
        {
            Console.WriteLine("Starting build...");

            if (!Directory.Exists(OUTPUT_PATH))
            {
                Directory.CreateDirectory(OUTPUT_PATH);
            }

            if (File.Exists(FULL_OUTPUT))
            {
                Console.WriteLine("Deleting old build...");
                File.Delete(FULL_OUTPUT);
            }

            ZipFile.CreateFromDirectory(DATA_PATH, FULL_OUTPUT);

            if (File.Exists(FULL_OUTPUT))
            {
                Console.WriteLine("Created output bundle successfully.");
            }
            else
            {
                Console.WriteLine("Couldn't create output bundle. Make sure you're running this app with correct permissions set.");
                return false;
            }

            return true;
        }

        private static bool ValidateData()
        {
            List<Clip> clips;
            List<Tab> tabs;
            string jsonFile;

            // Firstly, load the tabs json file, check its validity...
            Console.WriteLine(string.Format(LOADING_JSON_STR, "clips.json"));
            try
            {
                using (StreamReader sr = new StreamReader(DATA_PATH + "/clips.json"))
                {
                    // Read the stream to a string, and write the string to the console.
                    jsonFile = sr.ReadToEnd();
                    clips = JsonConvert.DeserializeObject<List<Clip>>(jsonFile);
                }
            }
            catch (IOException)
            {
                Console.WriteLine(string.Format(JSON_MISSING_STR, "clips.json"));
                return false;
            }

            // Secondly, load the tabs json file, check its validity...
            Console.WriteLine(string.Format(LOADING_JSON_STR, "tabs.json"));
            try
            {
                using (StreamReader sr = new StreamReader(DATA_PATH + "tabs.json"))
                {
                    // Read the stream to a string, and write the string to the console.
                    jsonFile = sr.ReadToEnd();
                    tabs = JsonConvert.DeserializeObject<List<Tab>>(jsonFile);
                }
            }
            catch (IOException)
            {
                Console.WriteLine(string.Format(JSON_MISSING_STR, "tabs.json"));
                return false;
            }

            // Validate the clips...
            foreach (Clip clip in clips)
            {
                if (!clip.validate())
                {
                    Console.WriteLine("A clip failed validation.");
                    return false;
                }
            }

            // Validate the tabs...
            List<string> tabNames = new List<string>();
            foreach (Tab tab in tabs)
            {
                if (tab.validate())
                {
                    if (tabNames.Contains(tab.title))
                    {
                        Console.WriteLine("Multiple tabs have the same name. This is not allowed.");
                        return false;
                    }
                    tabNames.Add(tab.title);
                }
                else
                {
                    Console.WriteLine("A tab failed validation.");
                    return false;
                }
            }

            // All validation succeeded...
            return true;
        }

        public class Clip
        {
            public string title = "";
            public string tab = "";
            public string imageTitle = "";
            public int orderInMenu = 0;
            public string clipTitle = "";

            public bool validate()
            {
                if (title == "")
                {
                    Console.WriteLine("Untitled clip found. Clips must have a name.");
                    return false;
                }

                if (tab == "")
                {
                    Console.WriteLine(string.Format("'{0}' clip doesn't have a valid tab name", title));
                    return false;
                }

                if (imageTitle == "")
                {
                    Console.WriteLine(string.Format("'{0}' clip doesn't have a valid image title", title));
                    return false;
                }

                if (clipTitle == "")
                {
                    Console.WriteLine(string.Format("'{0}' clip doesn't have a valid clip title", title));
                    return false;
                }

                return true;
            }
        }

        public class Tab
        {
            public string title = "";
            public int orderInMenu = 0;

            public bool validate()
            {
                if (title == "")
                {
                    Console.WriteLine("Untitled tab found. Tabs must have a name.");
                    return false;
                }

                return true;
            }
        }
    }
}
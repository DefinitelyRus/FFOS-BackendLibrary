using System.Text.Json;

namespace FFOS_Backend_Library;

public class MasterClass
{
    static void Main()
    {
        #region Initialization of variables
        string? DEFAULT_DIR = GetDirectory();
        string? jsonDir = GetDirectory("json");
        const string NAME_VERSION = "Menu Item Editor v1.0";
        const string HELP_ADD = "\nadd <unique_id> <item_name>* <price>* <image_file_name>*\n" +
            "    - Creates a new menu item with a unique ID. Don't use spaces here.\n" +
            "    - price has to be a number. Decimals are allowed.\n" +
            "    - image file name has to include the file extension, like .jpg or .png\n" +
            "    - Replace spaces ( ) with understores (_).\n" +
            "    * Optional parameters.\n" +
            "    Example: add chickenSandwich Chicken_Sandwich 45 Chicken Sandwich.jpg\n";
        const string HELP_EDIT = "\nedit <unique_id> <attribute> <value>\n" +
            "    - Edits an existing menu item.\n" +
            "    - Changeable attributes are as listed: unique_id, item_name, price, & image_file_name\n" +
            "    - Text values have to replace spaces ( ) with understores (_).\n" +
            "    - Price must be numerical only and CAN contain decimals.\n" +
            "    Example: edit image_file_name \"Icecream Sandwich.png\"\n";
        const string HELP_REMOVE = "\nremove <unique_id>\n" +
            "    - Deletes an existing menu item. THIS CANNOT BE UNDONE.\n" +
            "    - Use \"remove DELETE_ALL_ITEMS\" to empty the list. THIS CANNOT BE UNDONE.\n" +
            "    Example: remove goldenSandwich";
        const string HELP_LIST = "\nlist\n" +
            "    - Lists all existing menu items and their attributes.\n";
        const string HELP_ALL =
            "Commands:\n" + HELP_ADD + HELP_EDIT + HELP_REMOVE + HELP_LIST +
            "\nhelp\n    - Shows this list.";
        #endregion

        Console.WriteLine(NAME_VERSION + "\n\n" + "Use command \"help\" to list all commands.");
        Console.WriteLine("\n--------------------------------------------------------------------------------");
        while (true)
        {
            try
            {
                //Empty line
                Console.WriteLine();

                //Receives user input
                string? rawInput = Console.ReadLine();

                //Skips if the command is empty
                if (String.IsNullOrEmpty(rawInput.Replace(" ", ""))) continue;

                //Splits the inputs into an array to separate arguments.
                string[] inputSplit = rawInput.Split(' ');

                //Initializating a blank array.
                MenuItem[] menuArray = new MenuItem[1];

                switch (inputSplit[0])
                {
                    case "add":
                        {
                            #region Initialization of these variables.
                            string itemId = "", itemName = "", itemPriceString = "0", imageFileName = "";
                            menuArray = ParseJson(ReadJson(jsonDir));

                            try { itemId = inputSplit[1]; }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_ADD +
                                    "\nThis error message appeared because " +
                                    "unique_id is a required parameter but none was received.");
                                break;
                            }

                            try { itemName = inputSplit[2].Replace('_', ' '); }
                            catch (IndexOutOfRangeException) { }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_ADD +
                                "\nThis error message appeared because " +
                                "your item_name input is invalid.");
                                break;
                            }

                            try { itemPriceString = inputSplit[3]; }
                            catch (IndexOutOfRangeException) { }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_ADD +
                                "\nThis error message appeared because " +
                                "your price input is invalid.");
                                break;
                            }

                            try { imageFileName = inputSplit[4].Replace('_', ' '); }
                            catch (IndexOutOfRangeException) { }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_ADD +
                                "\nThis error message appeared because " +
                                "your image_file_name input is invalid.");
                                break;
                            }

                            //Setting attributes for MenuItem object.
                            MenuItem itemEntry = new MenuItem(itemId);
                            itemEntry.ItemName = itemName;
                            itemEntry.ImageFileName = imageFileName;

                            //Parse itemPriceString to float, put as Price attribute.
                            try { itemEntry.Price = float.Parse(itemPriceString); }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_ADD +
                                "\nThis error message appeared because " +
                                "your input cannot be parsed into a number.");
                                break;
                            }
                            #endregion

                            //Read from file
                            MenuItem[] extendedMenuArray;

                            //Creates a new array with 1 slot.
                            if (menuArray is null) extendedMenuArray = new MenuItem[1];
                            else //Reassigns to a new object with +1 length
                            {
                                extendedMenuArray = new MenuItem[menuArray.Length + 1];
                                for (int i = 0; i < menuArray.Length; i++)
                                    extendedMenuArray[i] = menuArray[i];
                            }

                            //Adds itemEntry to the end of extendedMenuArray
                            extendedMenuArray[^1] = itemEntry;

                            //Add jsonString to JSON file.
                            WriteJson(extendedMenuArray, jsonDir);

                            //End of command
                            Console.WriteLine("Menu item " + itemId + " added to menu.");
                            break;
                        }

                    case "edit":
                        {
                            #region Initialization of variables
                            menuArray = ParseJson(ReadJson(jsonDir));
                            string itemId = "", itemValue = "", itemAttribute = "";
                            try { itemId = inputSplit[1]; }
                            catch (IndexOutOfRangeException e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_EDIT +
                                    "\nThis error message appeared because " +
                                    "unique_id is a required parameter but none was received.");
                                break;
                            }
                        
                            try { itemAttribute = inputSplit[2]; }
                            catch (IndexOutOfRangeException e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_EDIT +
                                    "\nThis error message appeared because " +
                                    "attribute is a required parameter but none was received.");
                            }

                            try { itemValue = inputSplit[3].Replace('_', ' '); }
                            catch (IndexOutOfRangeException e)
                            {
                                Console.WriteLine(e.ToString() + "\n" + HELP_EDIT +
                                    "\nThis error message appeared because " +
                                    "value is a required parameter but none was received.");
                            }
                            #endregion

                            for (int i = 0; i < menuArray.Length; i++)
                            {
                                MenuItem item = menuArray[i];

                                if (item.ID == itemId)
                                {
                                    switch (itemAttribute)
                                    {
                                        case "unique_id":
                                            item.ID = itemValue;
                                            break;

                                        case "item_name":
                                            item.ItemName = itemValue;
                                            break;

                                        case "price":
                                            try { item.Price = float.Parse(itemValue); }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine(
                                                    "Invalid price value. Numbers and decimals only.");
                                            }
                                            break;

                                        case "image_file_name":
                                            item.ImageFileName = itemValue;
                                            break;

                                        default:
                                            Console.WriteLine(HELP_EDIT + "\n\"" +
                                                itemAttribute + "\" attribute does not exist.");
                                            break;
                                    }

                                    menuArray[i] = item;

                                    WriteJson(menuArray, jsonDir);

                                    Console.WriteLine(itemAttribute + " attribute of " +
                                        itemId + " was updated to \"" + itemValue + "\".");
                                }
                            }
                            break;
                        }

                    case "remove":
                        {
                            #region Initialization of variables
                            menuArray = ParseJson(ReadJson(jsonDir));
                            string itemId = "";

                            try { itemId = inputSplit[1]; }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine(HELP_REMOVE +
                                    "\nThis help message appeared because " +
                                    "unique_id is a required parameter but none was received.");
                                break;
                            }

                            MenuItem[] newMenu = new MenuItem[menuArray.Length - 1];
                            #endregion

                            //Iterates through the internal menu,
                            //assigns its contents to the new menu,
                            //but excludes one that matches the target.
                            for (int i = 0, i2 = 0; i < menuArray.Length; i++)
                            {
                                //Console.WriteLine(menuArray[i].ItemName);
                                if (menuArray[i].ID != itemId)
                                {
                                    Console.WriteLine("Added " + menuArray[i].ID);
                                    newMenu[i2] = menuArray[i];
                                    i2++;
                                }
                                else Console.WriteLine("Excluded " + menuArray[i].ID);
                            }

                            WriteJson(newMenu, jsonDir);

                            //What if the item doesn't exist?
                            //What if the list is empty?
                            Console.WriteLine("Removed " + itemId + " from the menu.");
                            break;
                        }

                    case "list":
                        {
                            menuArray = ParseJson(ReadJson(jsonDir));

                            if (menuArray == null)
                            {
                                Console.WriteLine("The menu is empty. Use command \"add\" to add new items.");
                                break;
                            }

                            foreach (MenuItem item in menuArray)
                            {
                                Console.WriteLine(
                                    item.ItemName +
                                    "\n    - ID: " + item.ID +
                                    "\n    - Price: " + item.Price +
                                    "\n    - Image: " + item.ImageFileName
                                    );
                            }
                            break;
                        }

                    case "help":
                        {
                            Console.WriteLine(HELP_ALL);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Invalid input. Try again or restart the program.");
            }
        }
    }

    /// <summary>
    /// Reads the target text file.
    /// </summary>
    /// <param name="filePath">This is the directory to the target file.
    ///"C:/Users/YOUR_NAME/Documents/file.txt" is an example of this.</param>
    /// <param name="testPrint">This will print a debug message to see if
    /// the target file exists and number of characters it contains.</param>
    /// <returns>A string retrieved from reading the target text file.</returns>
    public static string ReadFile (string filePath, bool testPrint = false)
    {
        string text;
        if (testPrint) Console.WriteLine("Attempting to read file from \"" + filePath + "\"...");

        try { text = File.ReadAllText(filePath); }
        catch { return ReadFile(TextFileNotFoundAutoFix(filePath), testPrint); }

        if (testPrint)
        {
            Console.WriteLine("Read success.");
            Console.WriteLine("File path: \"" + filePath + " contains " + text.Length + " characters.");
        }

        return text;
    }

    /// <summary>
    /// Simplifies the process of writing text into a file.
    /// </summary>
    /// <param name="filePath">This is the directory to the target file.
    ///"C:/Users/YOUR_NAME/Documents/file.txt" is an example of this.</param>
    /// <param name="content">The string to write to the file.</param>
    public static void WriteFile (string filePath, string content)
    {
        try { File.WriteAllText(filePath, content); }
        catch (FileNotFoundException) { WriteFile(TextFileNotFoundAutoFix(filePath), content); }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }

    /// <summary>
    /// Converts the JSON-formatted file to a C# Object.
    /// </summary>
    /// <param name="filePath">this will do stuff</param>
    /// <returns>An Object array based on the received JSON string.</returns>
    public static dynamic[] ReadJson(string filePath)
    {
        dynamic[]? objArray = null;
        try
        {
            string jsonAsString = ReadFile(filePath, false);
            objArray = JsonSerializer.Deserialize<Object[]>(jsonAsString);
        }
        catch (FileNotFoundException) { return ReadJson(TextFileNotFoundAutoFix(filePath)); }
        catch (Exception e) { Console.WriteLine(e.ToString()); }

        return objArray;
    }

    ///<summary>Converts the Object into a JSON-formatted string,
    ///which will then be saved into a TEXT or JSON file.</summary>
    ///<param name="obj">This is the object that will be converted to a JSON string.</param>
    ///<param name="filePath">This is the directory to the target file.
    ///"C:/Users/YOUR_NAME/Documents/file.txt" is an example of this.</param>
    public static void WriteJson (Object[] obj, string filePath)
    {
        try
        {
            string parsedJson = JsonSerializer.Serialize<Object[]>(
                obj, new JsonSerializerOptions { WriteIndented = true });
            WriteFile(filePath, parsedJson);
        }
        catch (FileNotFoundException)
        {
            WriteJson(obj, TextFileNotFoundAutoFix(filePath));
        }
        catch (Exception e) { Console.WriteLine(e.StackTrace); }
    }

    /// <summary>
    /// Converts the JSON object array into a MenuItem array.
    /// </summary>
    /// <param name="jsonArray">A parsed JSON object array.</param>
    /// <returns>A MenuItem array, identical in content with the JSON array inputted.</returns>
    public static MenuItem[] ParseJson(dynamic[] jsonArray) //Only works with MenuItem objs for now.
    {
        if (jsonArray == null) return null;
        //Read from file
        MenuItem[] menuArray = new MenuItem[jsonArray.Length];

        //Converts JsonElement Array to MenuItem Array.
        for (int i = 0; i < jsonArray.Length; i++)
        {
            JsonElement castableObj = jsonArray[i];
            menuArray[i] = castableObj.Deserialize<MenuItem>();
        }
        return menuArray;
    }
    
    /// <summary>
    /// A preset function used to retrieve the default path for the target.
    /// </summary>
    /// <param name="target">An optional parameter,
    /// set to the My Documents folder of your computer by default.</param>
    /// <returns>The directory of your target in string form.</returns>
    public static string GetDirectory(string target = "documents")
    {
        string dir = Directory.CreateDirectory(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments
                            )).FullName;
        switch (target)
        {
            case "documents":
                return dir;
            case "json":
                return dir + @"\FFOS\Menu.json";
            case "images":
                return dir + @"\FFOS\Images";
            default:
                return null;
        }
    }

    private static string TextFileNotFoundAutoFix(string filePath)
    {
        string[] pathSplit = filePath.Split('\\');
        string directoryNoFile = "";
        Console.WriteLine("\nFileNotFoundException detected. Creating " +
            pathSplit[^1] + " automatically...");

        //Starts from 2nd last. 1st last is always the target file, not a folder.
        for (int i = 0; i < pathSplit.Length - 1; i++)
        {
            directoryNoFile += pathSplit[i] + @"\";
        }

        //Creates all missing directories.
        Directory.CreateDirectory(directoryNoFile + @"\Images");

        //Creates the file at the target directory.
        File.CreateText(filePath).Close();

        //Returns the input argument for recursion purposes.
        return filePath;
    }

}
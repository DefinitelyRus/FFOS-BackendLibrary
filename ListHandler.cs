namespace FFOS_Backend_Library;

public class ListHandler
{
    private LinkedList<String[]> itemList = new LinkedList<String[]>();

    public ListHandler()
    {

    }

    /**<summary>
     * This will contain the code that retrieves the data from 2 lists
     * then combining them into a 2D array, then returns that array.
     * <br></br>
     * It converts the locally-stored LinkedList into a 2D array, then returns it.</summary>
     * The difference between the two are the way they function.
     * 
     * Linked lists are not primitive data types, like an integer or a string.
     * For example, a linked list uses nodes. Each node points to the next node.
     * Every node contains a primitive data type. As a result,
     * it can act like a list by adding another node like a lego brick.
     * 
     * Arrays aren't a data type in itself. Rather, it's a collection of
     * one primitive data type. Like a piece of paper with a set number
     * of blank lines, waiting to be occupied.
     * This means that arrays can't be expanded and instead have to be
     * remade with a larger size if you want to fit more contents.
     * 
     * As for why you'd want to use arrays despite its flaws,
     * it takes less steps to READ data from an array than with linked lists.
     * Conversely, linked lists are more versatile and easier to expand and edit.
     */
    public string[,] GetTableContents()
    {
        //UNTESTED


        //An empty 2D array used to contain the contents of the list using an array instead of a list.
        string[,] tableContents = new string[100, 2];

        //Used to keep track of what row the FOR loop is pointing at.
        int rowCounter = 0;

        foreach (string[] stringPair in itemList)
        {
            Console.WriteLine("Row " + rowCounter + " of " + itemList.Count);

            //Ends the loop if the first item of the pair is an empty string.
            if (stringPair[0] == "") break;

            //Assigns the Nth item of the pair to the 2D array.
            tableContents[rowCounter, 0] = stringPair[0]; //1st item
            tableContents[rowCounter, 1] = stringPair[1]; //2nd item
            rowCounter++; //Increases rowCounter by 1.
        }

        return tableContents;
    }

    /// <summary>
    /// Replaces <c>itemList</c> with the given list.
    /// </summary>
    public ListHandler SetTableContents(LinkedList<string[]> newItemList)
    {
        //UNTESTED
        itemList = newItemList;

        Console.WriteLine("Replaced the list contents.");

        return this;
    }

    /// <summary>
    /// Replaces the contents of <c>itemList</c> with the contents of the given array.
    /// <br></br>
    /// This will split the 2D array into X number of pairs.
    /// Each pair will then be added to <c>itemList</c>.
    /// </summary>
    /*
     * Notice how the method names are the same,
     * these commonly do the same thing but in different ways.
     * This is called method overloading.
     * This allows you to reuse methods with different
     * return types or input parameters.
     * 
     * The first version accepts LinkedLists as a parameter,
     * so it has to be coded to just replace itemList with new content.
     * 
     * This second version accepts 2D string arrays.
     * Since they aren't the same data type, we first have to work around it.
     * (Explanation is below.)
     */
    public ListHandler SetTableContents(string[,] newItemArray)
    {
        //UNTESTED

        Console.WriteLine("Replacing itemList contents...");

        itemList.Clear();
        /* First, it clears the list.
         * This way, we can just keep adding to the end of the list
         * without having to worry about the existing contents,
         * effectively replacing the list with another.
         */

        //We can then iterate through the list.
        for (int index = 0; index >= newItemArray.Length; index++)
        {
            /*
             * In this case, it's better to count the index rather than
             * to iterate through each item on a item by item basis.
             * 
             * Arrays rely on indexes to point at the data it contains.
             * >    newItemArray[index, 0]
             * The index is used here to indicate which row in the array to point at.
             * The 0 just refers to the 1st column.
             */
            string[] stringPair = new string[] { newItemArray[index, 0], newItemArray[index, 1] };
            //This line just creates a string array with 2 items in it.
            //It takes the 1st and 2nd item of the Nth row from newItemArray,
            //then puts it as its own 2-item array called stringPair.

            itemList.AddLast(stringPair); //It then adds stringPair to the itemList.

            Console.WriteLine("[" + index + "] Added \"" + stringPair[0] + "\" with price " + stringPair[1] + ".");
        }
        Console.WriteLine("Added all " + newItemArray.Length + " items to itemList.");

        return this;
    }

    /// <summary>
    /// Adds the given <c>itemName</c> and <c>itemPrice</c> into the <c>itemList</c>.
    /// This will also convert the <c>itemPrice</c> into a string
    /// due to the list only being able to store strings.
    /// </summary>
    public ListHandler AddTableContent(string itemName, int itemPrice)
    {
        //UNTESTED
        //Adds a the itemName and itemPrice (as string) to the end of the itemList.
        itemList.AddLast(new string[] {itemName, itemPrice.ToString()});

        Console.WriteLine("Added \"" + itemName + "\" with price " + itemPrice + " to itemList.");

        return this;
    }

    
}
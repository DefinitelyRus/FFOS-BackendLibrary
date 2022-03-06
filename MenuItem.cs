namespace FFOS_Backend_Library
{
    public class MenuItem
    {
        private string id = "";
        private string name = "";
        private float price = 0;
        private string imgFileName = "";

        public MenuItem(string id, string itemName = "", float price = 0, string imgFileName = "")
        {
            this.name = itemName;
            this.price = price;
            this.imgFileName = imgFileName;
        }

        //Item ID STRING: A unique ID for the item.
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        
        //Item Name STRING: The name of the item.
        //USE NEW GETTER/SETTERS
        public MenuItem setName(string name) { this.name = name; return this; }
        public string getName() { return this.name; }

        //Item Price FLOAT: The price of the item.
        //USE NEW GETTER/SETTERS
        public MenuItem setPrice(float price) { this.price = price; return this; }
        public float getPrice() { return this.price; }

        //Image File Name STRING: The name of the image file, including its extension name.
        //USE NEW GETTER/SETTERS
        public MenuItem setImgFileName(string imgFileName) { this.imgFileName = imgFileName; return this; }
        public string getImgFileName() { return this.imgFileName; }
        public string getImgFilePath()
        {
            //TODO: Make try-catch for missing file/folder.
            return Environment.ProcessPath + "/Images/" + this.imgFileName;
        }

    }
}

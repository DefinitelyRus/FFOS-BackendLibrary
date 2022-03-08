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
            this.id = id;
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
        public string ItemName
        {
            get { return name; }
            set { name = value; }
        }

        //Item Price FLOAT: The price of the item.
        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        //Image File Name STRING: The name of the image file, including its extension name.
        public string ImageFileName
        {
            get { return imgFileName; }
            set { imgFileName = value; } //Update Image attribute.
        }
        public string getImgFilePath()
        {
            //TODO: Make try-catch for missing file/folder.
            return Environment.ProcessPath + "/Images/" + this.imgFileName;
        }
    }
}

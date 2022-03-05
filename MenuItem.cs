namespace FFOS_Backend_Library
{
    public class MenuItem
    {
        private string name = "";
        private float price = 0;
        private string imgFileName = "";

        public MenuItem(string itemName, float price, string imgFileName)
        {
            this.name = itemName;
            this.price = price;
            this.imgFileName = imgFileName;
        }

        //Item Name STRING: The name of the item.
        public MenuItem setName(string name) { this.name = name; return this; }
        public string getName() { return this.name; }

        //Item Price FLOAT: The price of the item.
        public MenuItem setPrice(float price) { this.price = price; return this; }
        public float getPrice() { return this.price; }

        //Image File Name STRING: The name of the image file, including its extension name.
        public MenuItem setImgFileName(string imgFileName) { this.imgFileName = imgFileName; return this; }
        public string getImgFileName() { return this.imgFileName; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras;

namespace TheForest
{
    class Program
    {

        IWebDriver chrome;


       [SetUp]

       public void Star_Testing()
        {
            chrome = new ChromeDriver("C:\\belajarDriver");
            string website_name = "https://www.saucedemo.com/";
            chrome.Manage().Window.Maximize();
            chrome.Navigate().GoToUrl(website_name);
            Thread.Sleep(5000);

            string user_login = "standard_user";
            string pass_login = "secret_sauce";

            IWebElement username = chrome.FindElement(By.Id("user-name"));
            IWebElement password = chrome.FindElement(By.Id("password"));

            username.SendKeys(user_login);
            password.SendKeys(pass_login);

            IWebElement btn_Login = chrome.FindElement(By.Id("login-button"));
            btn_Login.Click();
        }

        [Test]
        public void Jumlah_Produk()
        {
            IWebElement Llb_product = chrome.FindElement(By.XPath("//*[@id='header_container']/div[2]/span"));

            IList<IWebElement> Inventory_items = chrome.FindElements(By.ClassName("inventory_item"));

            int Total_Inventory_Item;
            Total_Inventory_Item = Inventory_items.Count();

            Console.Write(Total_Inventory_Item);
        }

        [Test]
        public void Test_Assessment()
        {
            Thread.Sleep(1000);

            Random rnd = new Random();
            int no_Item = rnd.Next(1, 6);

            //product on home
            IWebElement item_one = chrome.FindElement(By.XPath("//*[@id='inventory_container']/div/div["+no_Item+"]"));
            IWebElement Product_Name_element = item_one.FindElement(By.ClassName("inventory_item_name"));
            IWebElement product_price = item_one.FindElement(By.ClassName("inventory_item_price"));
            string nama_product = Product_Name_element.Text.ToString();
            string price = product_price.Text.ToString();

            IWebElement btn_addToCart = item_one.FindElement(By.XPath(".//div[2]/div[2]/button"));
            btn_addToCart.Click();
            Thread.Sleep(5000);
  
            //Click Button Cart
            IWebElement btn_cart = chrome.FindElement(By.Id("shopping_cart_container"));
            btn_cart.Click();
            Thread.Sleep(2000);

            //product on chart
            IWebElement available_item = chrome.FindElement(By.ClassName("cart_item"));

            IWebElement cart_Product_name = available_item.FindElement(By.ClassName("inventory_item_name"));
            IWebElement product_cart_prize = available_item.FindElement(By.ClassName("inventory_item_price"));

            string cart_product = cart_Product_name.Text.ToString();
            string prize_cart_procut = product_cart_prize.Text.ToString();

            Assert.AreEqual(nama_product, cart_product, $"nama product = {nama_product} dan nama product dalam cart {cart_product}");
            Assert.AreEqual(price, prize_cart_procut, $"Product Prize = {price} but the actual result = {prize_cart_procut}");
            



        }

        [Test]
        public void InputKeranjang()
        {
            Thread.Sleep(2000);

            Random rnd = new Random();
            HashSet<int> uniqueNumbers = new HashSet<int>();
            List<string> elementTextList = new List<string>(); 
            int desiredCount = 3;
            


            while (uniqueNumbers.Count < desiredCount)
            {
                int randomNumber;
                do
                {
                    randomNumber = rnd.Next(1, 6); // Generates a random number between 1 and 100 (inclusive).
                }
                while (!uniqueNumbers.Add(randomNumber)); // Keep generating until you find a unique number.

                uniqueNumbers.Add(randomNumber);
            }

            string Single = "";

            foreach (int number in uniqueNumbers)
            {
                int no_Item = rnd.Next(1, 6);

                //product on home
                IWebElement item_one = chrome.FindElement(By.XPath("//*[@id='inventory_container']/div/div[" + number + "]"));
                IWebElement Product_Name_element = item_one.FindElement(By.ClassName("inventory_item_name"));
                IWebElement btn_addToCart = item_one.FindElement(By.XPath(".//div[2]/div[2]/button"));
                btn_addToCart.Click();
                Thread.Sleep(2000);

                Single = Product_Name_element.Text.ToString();
                elementTextList.Add(Single);
               

            }
            foreach (string text in elementTextList)
            {
                //Console.WriteLine(text);
            }




            //Click Button Cart
            IWebElement btn_cart = chrome.FindElement(By.Id("shopping_cart_container"));
            btn_cart.Click();
            Thread.Sleep(2000);
            List<string> list_produk_keranjang = new List<string>();


            IList<IWebElement> available_item = chrome.FindElements(By.ClassName("cart_item"));
            int jumlah_avaliable_Item = available_item.Count();
            foreach (IWebElement element in available_item)
            {
                IWebElement cart_Product_name = element.FindElement(By.ClassName("inventory_item_name"));
                string cart_product = cart_Product_name.Text.ToString();

                list_produk_keranjang.Add(cart_product);
                //Console.WriteLine(cart_product);

            }

            bool areEqual = elementTextList.SequenceEqual(list_produk_keranjang);

            if (areEqual)
            {
                Console.WriteLine("The two lists are equal.");
            }
            else
            {
                Console.WriteLine("The two lists are not equal.");
            }
        }


        [TearDown]
        public void ShutDown()
        {
            chrome.Quit();
        }
       
       
    }
}

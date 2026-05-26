using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Threading;

namespace ClassLibrary1
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test1_field()
        {
            HtmlElement element = null;

            Thread thread = new Thread(() =>
            {
                WebBrowser browser = new WebBrowser();
                browser.ScriptErrorsSuppressed = true;
                browser.Navigate("https://www.ebay.com");

                int waited = 0;
                while (browser.ReadyState != WebBrowserReadyState.Complete || waited < 3000)
                {
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                    waited += 100;
                }

                // Try multiple possible IDs for search field
                element = browser.Document.GetElementById("gh-ac");
                if (element == null)
                    element = browser.Document.GetElementById("search-input");
                if (element == null)
                    element = browser.Document.GetElementById("gh-search-input");

                browser.Dispose();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Assert.IsNotNull(element);
        }

        [TestMethod]
        public void Test2_search()
        {
            HtmlElement element = null;

            Thread thread = new Thread(() =>
            {
                WebBrowser browser = new WebBrowser();
                browser.ScriptErrorsSuppressed = true;
                browser.Navigate("https://www.ebay.com");

                int waited = 0;
                while (browser.ReadyState != WebBrowserReadyState.Complete || waited < 3000)
                {
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                    waited += 100;
                }

                // Try multiple possible IDs for search button
                element = browser.Document.GetElementById("gh-btn");
                if (element == null)
                    element = browser.Document.GetElementById("gh-search-btn");
                if (element == null)
                    element = browser.Document.GetElementById("search-btn");
                if (element == null)
                    element = browser.Document.GetElementById("gh-search-input");

                browser.Dispose();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Assert.IsNotNull(element);
        }
    }
}
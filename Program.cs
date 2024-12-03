using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumPruebaTienda
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            try
            {
                driver.Navigate().GoToUrl("file:///C:/Users/Dell/Desktop/Proyecto%20final%20kellyn/index.html");
                driver.Manage().Window.Maximize();

                // Desplazarse hacia abajo y hacia arriba
                desplazarhaciaabajo(driver);
                desplazarhaciararriba(driver);

                // Navegar a las diferentes secciones
                NavegarASeccion(driver, "Servicios");
                NavegarASeccion(driver, "Contactos");
                NavegarASeccion(driver, "Productos");
                NavegarASeccion(driver, "Inicio");
                // Agregar productos al carrito
                AbrirCarrito(driver);
                Agregarproductoalcarrito(driver, 1);
                Agregarproductoalcarrito(driver, 2);
                Agregarproductoalcarrito(driver, 3);
                AbrirCarrito(driver);

                // Eliminar un producto del carrito
                Eliminarunproductodelcarrito(driver, 1);
                AbrirCarrito(driver);

                // Vaciar el carrito
                Vaciarcarrito(driver);
                AbrirCarrito(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
        }

        // Función para navegar a una sección específica
        public static void NavegarASeccion(IWebDriver driver, string seccion)
        {
            try
            {
                // Usamos un selector basado en el nombre de la sección.
                IWebElement seccionButton = driver.FindElement(By.LinkText(seccion));
                seccionButton.Click();
                Console.WriteLine($"Navegando a la sección: {seccion}");
                Thread.Sleep(1000); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo navegar a la sección {seccion}: {ex.Message}");
            }
        }

        // Agregar producto al carrito
        public static void Agregarproductoalcarrito(IWebDriver driver, int productId)
        {
            try
            {
                IWebElement addButton = driver.FindElement(By.CssSelector($"a[data-id='{productId}']"));
                addButton.Click();
                Console.WriteLine($"Producto {productId} agregado al carrito.");
                Thread.Sleep(1000); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo agregar el producto {productId} al carrito: {ex.Message}");
            }
        }

        // Abre el carrito para comprobar los productos
        public static void AbrirCarrito(IWebDriver driver)
        {
            try
            {
                IWebElement carritoButton = driver.FindElement(By.Id("img-carrito"));
                carritoButton.Click();
                Console.WriteLine("Carrito abierto.");
                Thread.Sleep(1000); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo abrir el carrito: {ex.Message}");
            }
        }

        // Eliminar un producto del carrito
        public static void Eliminarunproductodelcarrito(IWebDriver driver, int productId)
        {
            try
            {
               
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(drv => drv.FindElement(By.Id("lista-carrito")));

   
                IWebElement removeButton = driver.FindElement(By.CssSelector($"#lista-carrito tr:nth-child({productId}) .remove-product"));
                removeButton.Click();
                Console.WriteLine($"Producto {productId} eliminado del carrito.");
                Thread.Sleep(1000); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo eliminar el producto {productId} del carrito: {ex.Message}");
            }
        }

        // Vaciar todo el carrito
        public static void Vaciarcarrito(IWebDriver driver)
        {
            try
            {
                IWebElement emptyCartButton = driver.FindElement(By.Id("vaciar-carrito"));
                emptyCartButton.Click();
                Console.WriteLine("Carrito vaciado.");
                Thread.Sleep(1000); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo vaciar el carrito: {ex.Message}");
            }
        }

        public static void desplazarhaciaabajo(IWebDriver driver, int steps = 10, int scrollAmount = 300, int pauseTime = 2000)
        {
            for (int i = 0; i < steps; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy(0, {scrollAmount});");
                Thread.Sleep(pauseTime); 
            }
        }

        // Función para desplazarse hacia arriba lentamente
        public static void desplazarhaciararriba(IWebDriver driver, int steps = 10, int scrollAmount = 300, int pauseTime = 2000)
        {
            for (int i = 0; i < steps; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy(0, {-scrollAmount});");
                Thread.Sleep(pauseTime); 
            }
        }
    }
}

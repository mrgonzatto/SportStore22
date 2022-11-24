using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        public IStoreRepository repository { get; set; }
        public int PageSize = 1;

        public HomeController(IStoreRepository storeRepo)
        {
            repository = storeRepo;
        }

        [HttpGet]
        public IActionResult Index() => View(repository.Products);

        [HttpGet]
        public ViewResult PagedProducts(string category, int productPage = 1)
            => View(
                new ProductsListViewModel
                {
                    Products = repository.Products
                                .Where(p => p.Category == null || p.Category == category)
                                .OrderBy(p => p.ProductID)
                                .Skip((productPage - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo { 
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    }
                }
            );
    }
}
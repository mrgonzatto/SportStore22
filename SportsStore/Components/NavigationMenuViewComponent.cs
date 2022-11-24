﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {

            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(
                repository.Products
                .Select(p => p.Category)
                .Distinct()        
                .OrderBy( p => p )
            );
        }
    }
}

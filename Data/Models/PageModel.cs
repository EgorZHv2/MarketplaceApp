﻿namespace Data.Models
{
    public class PageModel<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsOnPage { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
}
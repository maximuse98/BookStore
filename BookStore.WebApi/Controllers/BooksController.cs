﻿using BookStore.Core;
using BookStore.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;

        public BooksController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddBook(Book book)
        {
            try
            {
                await bookService.CreateBook(book.Name, book.Description, book.GenreId);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            try
            {
                Book book = await bookService.GetBook(bookId);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

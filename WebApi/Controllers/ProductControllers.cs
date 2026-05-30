using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>();

    // GET: Retreive all products
    [HttpGet]
    public ActionResult<List<Product>> GetAll() => products;

    // GET by ID: Retreive a single product by ID
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        return product != null ? Ok(product) : NotFound();
    }

    // POST: Add a new product
    [HttpPost]
    public ActionResult<Product> Create(Product newProduct)
    {
        newProduct.Id = products.Count + 1;
        products.Add(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
    }

    // PUT: Update an existing product
    [HttpPut("{id}")]
    public ActionResult Update(int id, Product updateProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        product.Name = updateProduct.Name;
        product.Description = updateProduct.Description;
        product.Price = updateProduct.Price;
        return Ok(product);
    }

    // DELETE: Remove a product
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        products.Remove(product);
        return NoContent();
    }
}
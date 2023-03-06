using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs;

public class BlogRepository : IBlogRepository
{
    private readonly BlogDbContext _context;

    public BlogRepository(BlogDbContext context)
    {
        _context = context;
    }


    public async Task<Post> GetPostAsync(
      int year,
      int month,
      string slug,
      CancellationToken cancellationToken = default)
    {
        IQueryable<Post> postsQuery = _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author);

        if(year > 0)
        {
            postsQuery = postsQuery.Where(x=>x.PostedDate.Year == year);
        }


        if (month > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
        }

        if(!string.IsNullOrEmpty(slug))
        {
            postsQuery=postsQuery.Where(x=>x.UrlSlug == slug);
        }

        return await postsQuery.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IList<Post>> GetPopularArticlesAsync(
        int numPosts,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .OrderByDescending(x => x.ViewCount)
            .Take(numPosts)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsPostSlugExixtedAsync(
        int postID, string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .AnyAsync(x => x.Id != postID && x.UrlSlug == slug, cancellationToken);
            
    }


    public async Task<bool> IsCategorySlugExixtedAsync(
    int categoryId, string slug,
    CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .AnyAsync(x => x.Id != categoryId && x.UrlSlug == slug, cancellationToken);

    }

    public async Task IncreaseViewCountAsync(
        int postId,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<Post>()
            .Where(x => x.Id == postId)
            .ExecuteUpdateAsync(p =>
            p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1), cancellationToken);
    }


    public async Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categories = _context.Set<Category>();

        if (showOnMenu)
        {
            categories = categories.Where(x => x.ShowOnMenu);
        }

        return await categories
            .OrderBy(x => x.Name)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default)
    {
        var tagQuery = _context.Set<Tag>()
            .Select(x => new TagItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                PostCount = x.posts.Count(p => p.Published)
            });

        return await tagQuery
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    public async Task<Tag> SeekTagWithUrlslugAync(string slugTag, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Tag>()
            .Where(x=>x.UrlSlug== slugTag)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IList<TagItem>> GetListTagAndAmountOfPostInTagAsync(CancellationToken cancellationToken = default)
    {
        var listTag = _context.Set<Tag>()
            .Select(t => new TagItem()
            {               
                Name = t.Name,               
                PostCount=t.posts.Count()
            });
        return await listTag
            .ToListAsync(cancellationToken);
    }

    public async Task<Category> SeekCategoryAsync(string slugCategory, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .Where(x=>x.UrlSlug == slugCategory)
            .FirstOrDefaultAsync (cancellationToken);
    }

    public async Task<Category> SeekCategoryByIdAsync(int categoryId, CancellationToken cancellation = default)
    {
        return await _context.Set<Category>()
            .Where(x => x.Id == categoryId)
            .FirstOrDefaultAsync(cancellation);
    }

    public async Task RemoveTagByIdAsync(int Id, CancellationToken cancellationToken = default)
    {
        _context.Database.ExecuteSqlRawAsync("DELETE FROM PostTags WHERE TagsId = "+Id);
    }

    public async Task AddOrUpdateCategoryAsysn(Category category, CancellationToken cancellationToken = default)
    {
        if (IsCategorySlugExixtedAsync(category.Id, category.UrlSlug).Result)
        { Console.WriteLine("da ton tai"); }
        else if (category.Id > 0)
        {
            await _context.Set<Category>()
                .Where(x => x.Id == category.Id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(a=>a.Name, category.Name)
                    .SetProperty(a=>a.Description, category.Description)
                    .SetProperty(a=>a.ShowOnMenu,category.ShowOnMenu)
                    .SetProperty(a=>a.Posts, category.Posts), cancellationToken);
        }
        else
        {
            _context.Categoties.Add(category);
            _context.SaveChanges();
        }
    }
}

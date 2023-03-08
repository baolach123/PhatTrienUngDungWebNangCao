﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Core.Constants;
using TatBlog.Core.Collections;



namespace TatBlog.Services.Blogs
{
    public interface IBlogRepository
    {
        Task<Post> GetPostAsync(
            int year,
            int month,
            string slug,
            CancellationToken cancellationToken = default);

        Task<IList<Post>> GetPopularArticlesAsync(
            int numPosts,
            CancellationToken cancellationToken = default);

        Task<bool> IsPostSlugExixtedAsync(
            int postID, string slug,
            CancellationToken cancellationToken = default);

        Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken = default);

        Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = false,
            CancellationToken cancellationToken = default);

        Task<IPagedList<TagItem>> GetPagedTagsAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<Tag> SeekTagWithUrlslugAync(
            string slugTag,
            CancellationToken cancellationToken = default);    
        
        Task<IList<TagItem>> GetListTagAndAmountOfPostInTagAsync(
            CancellationToken cancellationToken = default);

        Task<Category> SeekCategoryAsync(
            string slugCategory,
            CancellationToken cancellationToken = default);

        Task<Category> SeekCategoryByIdAsync(
            int categoryId,
            CancellationToken cancellation = default);

        Task RemoveTagByIdAsync(
            int Id,
            CancellationToken cancellationToken = default);

        Task AddOrUpdateCategoryAsysc(
            Category category, CancellationToken cancellationToken = default);

        Task RemoveCategoryByIdAsync(
            int Id, CancellationToken cancellationToken = default);

        Task CheckExistCategoryAsync(Category category,
    CancellationToken cancellationToken = default);


        Task<IPagedList<CategoryItem>> GetPagingCategoryAsync(IPagingParams pagingParams,
        CancellationToken cancellationToken = default);


        Task<Post> SeekPostByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task AddOrUpdatePostAsysc(Post postt,
            CancellationToken cancellationToken = default);

        Task ChangeStatusPublishAsync(int postId,
            CancellationToken cancellationToken=default);

        Task<IList<Post>> GetRandomNPostAsync(int n, 
            CancellationToken cancellationToken = default);

        Task<IList<Post>> SeekAllPostAsync(PostQuery postQuery,
            CancellationToken cancellationToken = default);

        Task<int> CountNumberPostAsync(PostQuery postQuery,
            CancellationToken cancellationToken = default);

        Task<IPagedList<Post>> SeekPagingPostAsync(PostQuery postQuery,
            IPagingParams pagingParams, CancellationToken cancellationToken=default);

        Task<IPagedList<Post>> GetPagedPostsAsync(
        PostQuery postQuery,
        int pageNumber = 1,
        int pageSize = 1,
        CancellationToken cancellationToken = default);
    }
}

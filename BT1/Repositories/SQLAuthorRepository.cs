using Microsoft.EntityFrameworkCore;
using BT1.Data;
using BT1.Models.Domain;
using BT1.Models.DTO;
using BT1.Repositories;


namespace BT1.Repositories
{
    // Lớp SQLAuthorRepository triển khai giao diện IAuthorRepository.
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AuthorDTO> GetAllAuthors()
        {
            throw new NotImplementedException();
        }
        // Phương thức GetAllAuthors() - Lấy tất cả tác giả từ CSDL
        public List<AuthorDTO> GellAllAuthors()
        {
            var allAuthors = _dbContext.Authors.Select(author => new AuthorDTO()
            {
                Id = author.Id,
                FullName = author.FullName
            }).ToList();
            return allAuthors;
        }

        // Phương thức GetAuthorById(int id) - Lấy một tác giả theo ID
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (author == null)
            {
                return null;
            }
            // Ánh xạ từ Domain Model sang DTO
            var authorNoIdDTO = new AuthorNoIdDTO()
            {
                FullName = author.FullName
            };
            return authorNoIdDTO;
        }

        // Phương thức AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO) - Thêm một tác giả mới
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            // Ánh xạ từ DTO sang Domain Model
            var authorDomainModel = new Authors
            {
                FullName = addAuthorRequestDTO.FullName
            };
            // Thêm tác giả vào CSDL
            _dbContext.Authors.Add(authorDomainModel);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;
        }

        // Phương thức UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO) - Cập nhật thông tin tác giả
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (authorDomain == null)
            {
                return null;
            }
            authorDomain.FullName = authorNoIdDTO.FullName;
            _dbContext.SaveChanges();

            // Ánh xạ lại Domain Model đã cập nhật sang DTO
            var updatedAuthorDTO = new AuthorNoIdDTO()
            {
                FullName = authorDomain.FullName
            };
            return updatedAuthorDTO;
        }

        // Phương thức DeleteAuthorById(int id) - Xóa một tác giả
        public Authors? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (authorDomain != null)
            {
                _dbContext.Authors.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return authorDomain;
        }

        
    }
}
using BT1.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BT1.Data;
using BT1.Models.DTO;
using BT1.Repositories;

namespace BT1.Repositories
{
    // Lớp SQLPublisherRepository triển khai giao diện IPublisherRepository.
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Publisher GetPublisherByName(string name)
        {
            return _dbContext.Publishers.FirstOrDefault(p => p.Name == name);
        }
        // Phương thức GetAllPublishers() - Lấy tất cả nhà xuất bản từ CSDL
        public List<PublisherDTO> GetAllPublishers()
        {
            var allPublishers = _dbContext.Publishers.Select(publisher => new PublisherDTO()
            {
                Id = publisher.Id,
                Name = publisher.Name
            }).ToList();
            return allPublishers;
        }

        // Phương thức GetPublisherById(int id) - Lấy một nhà xuất bản theo ID
        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisher == null)
            {
                return null;
            }
            // Ánh xạ từ Domain Model sang DTO
            var publisherNoIdDTO = new PublisherNoIdDTO()
            {
                Name = publisher.Name
            };
            return publisherNoIdDTO;
        }

        // Phương thức AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO) - Thêm một nhà xuất bản mới
        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            // Ánh xạ từ DTO sang Domain Model
            var publisherDomainModel = new Publisher
            {
                Name = addPublisherRequestDTO.Name
            };
            // Thêm nhà xuất bản vào CSDL
            _dbContext.Publishers.Add(publisherDomainModel);
            _dbContext.SaveChanges();
            return addPublisherRequestDTO;
        }

        // Phương thức UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO) - Cập nhật thông tin nhà xuất bản
        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain == null)
            {
                return null;
            }
            publisherDomain.Name = publisherNoIdDTO.Name;
            _dbContext.SaveChanges();

            // Ánh xạ lại Domain Model đã cập nhật sang DTO
            var updatedPublisherDTO = new PublisherNoIdDTO()
            {
                Name = publisherDomain.Name
            }; return updatedPublisherDTO;
        }

        // Phương thức DeletePublisherById(int id) - Xóa một nhà xuất bản
        public Publisher? DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
            }
            return publisherDomain;
        }

        public bool PublisherExists(int publisherId)
        {

            return _dbContext.Publishers.Any(p => p.Id == publisherId);
        }
    }
}
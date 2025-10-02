using BT1.Models.Domain;
using BT1.Models.DTO;

namespace BT1.Repositories
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherNoIdDTO GetPublisherById(int id);
        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
        Publisher? DeletePublisherById(int id);
    }
}


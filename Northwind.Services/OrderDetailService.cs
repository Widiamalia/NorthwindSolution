using AutoMapper;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrderDetailService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(OrderDetailDto orderDetailDto)
        {
            var edit = _mapper.Map<OrderDetail>(orderDetailDto);
            _repositoryManager.OrderDetailRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllCartItem(string custId, bool trackChanges)
        {
            var orderDetailModel = await _repositoryManager.OrderDetailRepository.GetAllCartItem(custId, trackChanges);
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetailModel);
            return orderDetailDto;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllOrderDetail(bool trackChanges)
        {
            var orderDetailModel = await _repositoryManager.OrderDetailRepository.GetAllOrderDetail(trackChanges);
            var orderDetailDto = _mapper.Map <IEnumerable<OrderDetailDto>>(orderDetailModel);
            return orderDetailDto;
        }

        public async Task<OrderDetailDto> GetOrderDetail(int orderId, int productId, bool trackChanges)
        {
            var orderDetailModel = await _repositoryManager.OrderDetailRepository.GetOrderDetail(orderId, productId, trackChanges);
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetailModel);
            return orderDetailDto;
        }

        public async Task<OrderDetailDto> GetOrderDetailById(int orderId, bool trackChanges)
        {
            var orderDetailModel = await _repositoryManager.OrderDetailRepository.GetOrderDetailById(orderId, trackChanges);
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetailModel);
            return orderDetailDto;
        }

        public void Insert(OrderDetailForCreateDto orderDetailForCreateDto)
        {
            var newData = _mapper.Map<OrderDetail>(orderDetailForCreateDto);
            _repositoryManager.OrderDetailRepository.Insert(newData);
            _repositoryManager.Save();
        }

        public void Remove(OrderDetailDto orderDetailDto)
        {
            var remove = _mapper.Map<OrderDetail>(orderDetailDto);
            _repositoryManager.OrderDetailRepository.Remove(remove);
            _repositoryManager.Save();
        }
    }
}

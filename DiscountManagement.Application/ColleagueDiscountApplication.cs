using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedDiscount);

            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(discount);
            _colleagueDiscountRepository.Save();
            return operation.Success();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(command.Id);
            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_colleagueDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedDiscount);

            discount.Edit(command.ProductId , command.DiscountRate);
            _colleagueDiscountRepository.Save();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Remove();
            _colleagueDiscountRepository.Save();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Restore();
            _colleagueDiscountRepository.Save();
            return operation.Success();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}

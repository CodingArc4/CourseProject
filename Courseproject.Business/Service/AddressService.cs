using AutoMapper;
using Courseproject.Business.Exceptions;
using Courseproject.Business.Validation;
using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Interface;
using Courseproject.Common.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Business.Service;

public class AddressService : IAddressService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Address> AddressRepository { get; }
    private AddressCreateValidator AddressCreateValidator { get; }
    private AddressUpdateValidator AddressUpdateValidator { get; }

    public AddressService(IMapper mapper, IGenericRepository<Address> addressRepository,
        AddressUpdateValidator addressUpdateValidator,AddressCreateValidator addressCreateValidator)
    {
        Mapper = mapper;
        AddressRepository = addressRepository;
        AddressUpdateValidator = addressUpdateValidator;
        AddressCreateValidator = addressCreateValidator;
    }

    public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
    {
        await AddressCreateValidator.ValidateAndThrowAsync(addressCreate); 

        var entity = Mapper.Map<Address>(addressCreate);
        await AddressRepository.InsertAsync(entity);
        await AddressRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var entity = await AddressRepository.getByIdAsync(addressDelete.id,(address)=> address.Employees);

        if (entity == null)
            throw new AddressNotFoundException(addressDelete.id);

        if (entity.Employees.Count > 0)
            throw new DependendEmployeeExistException(entity.Employees);

        AddressRepository.Delete(entity);
        await AddressRepository.SaveChangesAsync();
    }

    public async Task<AddressGet> GetAddressAsync(int id)
    {
        var entity = await AddressRepository.getByIdAsync(id);

        if(entity == null)
            throw new AddressNotFoundException(id);

        return Mapper.Map<AddressGet>(entity);
    }

    public async Task<List<AddressGet>> GetAddressesAsync()
    {
        var entities = await AddressRepository.GetAsync(null, null);
        return Mapper.Map<List<AddressGet>>(entities);
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
        await AddressUpdateValidator.ValidateAndThrowAsync(addressUpdate);

        var exsistingAddress = await AddressRepository.getByIdAsync(addressUpdate.Id);

        if (exsistingAddress == null)
            throw new AddressNotFoundException(addressUpdate.Id);

        var entity = Mapper.Map<Address>(addressUpdate);
        AddressRepository.Update(entity);
        await AddressRepository.SaveChangesAsync();
    }
}

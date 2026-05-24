using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ProductionSystem.Domain.Utilities;

namespace ProductionSystem.Infrastructure.Data.Seeder
{
    public class PermissionSeeder : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission { Id = RoleChecker.Home.ToValue(), Title = RoleChecker.Home.ToDisplay(), ParentId = null },
                new Permission { Id = RoleChecker.BasicInformation.ToValue(), Title = RoleChecker.BasicInformation.ToDisplay(), ParentId = null },
                new Permission { Id = RoleChecker.Production.ToValue(), Title = RoleChecker.Production.ToDisplay(), ParentId = null },
                new Permission { Id = RoleChecker.MyHome.ToValue(), Title = RoleChecker.MyHome.ToDisplay(), ParentId = null },

                // Users
                new Permission { Id = RoleChecker.User.ToValue(), Title = RoleChecker.User.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreateUser.ToValue(), Title = RoleChecker.CreateUser.ToDisplay(), ParentId = RoleChecker.User.ToValue() },
                new Permission { Id = RoleChecker.EditUser.ToValue(), Title = RoleChecker.EditUser.ToDisplay(), ParentId = RoleChecker.User.ToValue() },
                new Permission { Id = RoleChecker.DeleteUser.ToValue(), Title = RoleChecker.DeleteUser.ToDisplay(), ParentId = RoleChecker.User.ToValue() },


                // Roles
                new Permission { Id = RoleChecker.Role.ToValue(), Title = RoleChecker.Role.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreateRole.ToValue(), Title = RoleChecker.CreateRole.ToDisplay(), ParentId = RoleChecker.Role.ToValue() },
                new Permission { Id = RoleChecker.EditRole.ToValue(), Title = RoleChecker.EditRole.ToDisplay(), ParentId = RoleChecker.Role.ToValue() },
                new Permission { Id = RoleChecker.DeleteRole.ToValue(), Title = RoleChecker.DeleteRole.ToDisplay(), ParentId = RoleChecker.Role.ToValue() },

                // Personnel
                new Permission { Id = RoleChecker.Personnel.ToValue(), Title = RoleChecker.Personnel.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreatePersonnel.ToValue(), Title = RoleChecker.CreatePersonnel.ToDisplay(), ParentId = RoleChecker.Personnel.ToValue() },
                new Permission { Id = RoleChecker.EditPersonnel.ToValue(), Title = RoleChecker.EditPersonnel.ToDisplay(), ParentId = RoleChecker.Personnel.ToValue() },
                new Permission { Id = RoleChecker.DeletePersonnel.ToValue(), Title = RoleChecker.DeletePersonnel.ToDisplay(), ParentId = RoleChecker.Personnel.ToValue() },

                // Customers
                new Permission { Id = RoleChecker.Customer.ToValue(), Title = RoleChecker.Customer.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreateCustomer.ToValue(), Title = RoleChecker.CreateCustomer.ToDisplay(), ParentId = RoleChecker.Customer.ToValue() },
                new Permission { Id = RoleChecker.EditCustomer.ToValue(), Title = RoleChecker.EditCustomer.ToDisplay(), ParentId = RoleChecker.Customer.ToValue() },
                new Permission { Id = RoleChecker.DeleteCustomer.ToValue(), Title = RoleChecker.DeleteCustomer.ToDisplay(), ParentId = RoleChecker.Customer.ToValue() },

                // Units
                new Permission { Id = RoleChecker.Unit.ToValue(), Title = RoleChecker.Unit.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreateUnit.ToValue(), Title = RoleChecker.CreateUnit.ToDisplay(), ParentId = RoleChecker.Unit.ToValue() },
                new Permission { Id = RoleChecker.EditUnit.ToValue(), Title = RoleChecker.EditUnit.ToDisplay(), ParentId = RoleChecker.Unit.ToValue() },
                new Permission { Id = RoleChecker.DeleteUnit.ToValue(), Title = RoleChecker.DeleteUnit.ToDisplay(), ParentId = RoleChecker.Unit.ToValue() },

                // Products
                new Permission { Id = RoleChecker.Product.ToValue(), Title = RoleChecker.Product.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreateProduct.ToValue(), Title = RoleChecker.CreateProduct.ToDisplay(), ParentId = RoleChecker.Product.ToValue() },
                new Permission { Id = RoleChecker.EditProduct.ToValue(), Title = RoleChecker.EditProduct.ToDisplay(), ParentId = RoleChecker.Product.ToValue() },
                new Permission { Id = RoleChecker.DeleteProduct.ToValue(), Title = RoleChecker.DeleteProduct.ToDisplay(), ParentId = RoleChecker.Product.ToValue() },

                // Parameters
                new Permission { Id = RoleChecker.Parameter.ToValue(), Title = RoleChecker.Parameter.ToDisplay(), ParentId = RoleChecker.BasicInformation.ToValue() },
                new Permission { Id = RoleChecker.CreateParameter.ToValue(), Title = RoleChecker.CreateParameter.ToDisplay(), ParentId = RoleChecker.Parameter.ToValue() },
                new Permission { Id = RoleChecker.EditParameter.ToValue(), Title = RoleChecker.EditParameter.ToDisplay(), ParentId = RoleChecker.Parameter.ToValue() },
                new Permission { Id = RoleChecker.DeleteParameter.ToValue(), Title = RoleChecker.DeleteParameter.ToDisplay(), ParentId = RoleChecker.Parameter.ToValue() },


                // ParameterValues
                new Permission { Id = RoleChecker.ParameterValue.ToValue(), Title = RoleChecker.ParameterValue.ToDisplay(), ParentId = RoleChecker.Parameter.ToValue() },
                new Permission { Id = RoleChecker.CreateParameterValue.ToValue(), Title = RoleChecker.CreateParameterValue.ToDisplay(), ParentId = RoleChecker.ParameterValue.ToValue() },
                new Permission { Id = RoleChecker.EditParameterValue.ToValue(), Title = RoleChecker.EditParameterValue.ToDisplay(), ParentId = RoleChecker.ParameterValue.ToValue() },
                new Permission { Id = RoleChecker.DeleteParameterValue.ToValue(), Title = RoleChecker.DeleteParameterValue.ToDisplay(), ParentId = RoleChecker.ParameterValue.ToValue() },

                // Orders
                new Permission { Id = RoleChecker.Order.ToValue(), Title = RoleChecker.Order.ToDisplay(), ParentId = RoleChecker.Production.ToValue() },
                new Permission { Id = RoleChecker.CreateOrder.ToValue(), Title = RoleChecker.CreateOrder.ToDisplay(), ParentId = RoleChecker.Order.ToValue() },
                new Permission { Id = RoleChecker.EditOrder.ToValue(), Title = RoleChecker.EditOrder.ToDisplay(), ParentId = RoleChecker.Order.ToValue() },
                new Permission { Id = RoleChecker.DeleteOrder.ToValue(), Title = RoleChecker.DeleteOrder.ToDisplay(), ParentId = RoleChecker.Order.ToValue() },

                // ProductionReceipts
                new Permission { Id = RoleChecker.ProductionReceipt.ToValue(), Title = RoleChecker.ProductionReceipt.ToDisplay(), ParentId = RoleChecker.Production.ToValue() },
                new Permission { Id = RoleChecker.CreateProductionReceipt.ToValue(), Title = RoleChecker.CreateProductionReceipt.ToDisplay(), ParentId = RoleChecker.Production.ToValue() },
                new Permission { Id = RoleChecker.EditProductionReceipt.ToValue(), Title = RoleChecker.EditProductionReceipt.ToDisplay(), ParentId = RoleChecker.Production.ToValue() },
                new Permission { Id = RoleChecker.DeleteProductionReceipt.ToValue(), Title = RoleChecker.DeleteProductionReceipt.ToDisplay(), ParentId = RoleChecker.Production.ToValue() },

                //Report
                new Permission { Id = RoleChecker.Report.ToValue(), Title = RoleChecker.Report.ToDisplay(), ParentId = RoleChecker.Production.ToValue() }

            );
        }

    }
}


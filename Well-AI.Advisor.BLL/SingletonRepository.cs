using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL
{
    public interface ISingleton
    {
        IServiceVehicleBusiness serviceVehicleBusiness { get; }
        ISenderAndReceiverBusiness senderAndReceiverBusiness { get; }
        IAuctionProposalBusiness auctionProposalBusiness { get; }
        IProjectBusiness projectBusiness { get; }
        int savechange();

    }
    public class Singleton: ISingleton
    {
        private readonly WebAIAdvisorContext _context;
        private readonly UserManager<WellIdentityUser> _userManager;

        public Singleton(WebAIAdvisorContext context, UserManager<WellIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        private IProjectBusiness project;
        public IProjectBusiness projectBusiness
        {
            get
            {
                if (project == null)
                {
                    project = new ProjectBusiness(_context, _userManager);
                }
                return project;
            }
        }

        private IServiceVehicleBusiness serviceVehicle;
        public IServiceVehicleBusiness serviceVehicleBusiness
        {
            get
            {
                if (serviceVehicle == null)
                {
                    serviceVehicle = new ServiceVehicleBusiness(_context, _userManager);
                }
                return serviceVehicle;
            }
        }

        private ISenderAndReceiverBusiness senderAndReceiver;
        public ISenderAndReceiverBusiness senderAndReceiverBusiness
        {
            get
            {
                if (senderAndReceiver == null)
                {
                    senderAndReceiver = new SenderAndReceiverBusiness(_context, _userManager);
                }
                return senderAndReceiver;
            }
        }

        private IAuctionProposalBusiness auctionProposal;
        public IAuctionProposalBusiness auctionProposalBusiness
        {
            get
            {
                if (auctionProposal == null)
                {
                    auctionProposal = new AuctionProposalBusiness(_context, _userManager);
                }
                return auctionProposal;
            }
        }

        public int savechange()
        {
            return _context.SaveChanges();
        }
    }
}

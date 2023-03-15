using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Application.DTOs;
using Service.Application.ProductFeatures.Commands;
using Service.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.TransDataFeature.Queries
{
    //IRequest<List<DataCustom>>
    public class SearchDetail : IRequest
    {
        public int op { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgnetName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public class GetDataWithSearch : IRequestHandler<SearchDetail>
    {
        private readonly AestrainingContext _context;
        public GetDataWithSearch(AestrainingContext context)
        {
            _context = context;
        }
        public async Task Handle(SearchDetail keySearch, CancellationToken cancellationToken)
        {
            var data = await _context.TransactionDatas.FirstOrDefaultAsync(x => x.ClosingAgtBizName == keySearch.AgnetName);
            //return null;
        }
    }
}

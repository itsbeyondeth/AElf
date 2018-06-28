﻿using System;
using System.Threading.Tasks;
using AElf.Kernel.Managers;
using AElf.Kernel.Services;
using AElf.Kernel.Types;

namespace AElf.Kernel.BlockValidationFilters
{
    public class ChainContextValidationFilter : IBlockValidationFilter
    {
        private readonly IBlockManager _blockManager;

        public ChainContextValidationFilter(IBlockManager blockManager)
        {
            _blockManager = blockManager;
        }

        public async Task<ValidationError> ValidateBlockAsync(IBlock block, IChainContext context)
        {
            return ValidationError.Success;

            /*
                1' block height
                2' previous block hash
            */

            var index = block.Header.Index;
            var previousBlockHash = block.Header.PreviousBlockHash;

            // return success if genesis block
            if (index == 0 && previousBlockHash.Equals(Hash.Zero))
                return ValidationError.Success;

            var currentChainHeight = context.BlockHeight;
            var currentPreviousBlockHash = context.BlockHash;

            // other block needed before this one
            if (index > currentChainHeight)
                return ValidationError.OrphanBlock;
            
            // can be added to chain
            if (currentChainHeight == index && currentPreviousBlockHash.Equals(previousBlockHash))
                return ValidationError.Success;

            if (index < currentChainHeight)
                return ValidationError.AlreadyExecuted;
            
            // can not be added to chain with wrong prvious hash or wrong index
            /*if (currentChainHeight != index ^ currentPreviousBlockHash.Equals(previousBlockHash))
                return ValidationError.InvalidBlcok;*/
            
            // todo : manage orphan blocks
            // orphan block
            /*var pb = await _blockManager.GetBlockAsync(previousBlockHash);

            // 
            if (pb == null)
                return ValidationError.OrphanBlock;
            
            var pbHeight = pb.Header.Index;
            if (pbHeight + 1 != index)
            {
                return ValidationError.InvalidBlcok;
            }*/
            
            return ValidationError.OrphanBlock;
        }
    }
}
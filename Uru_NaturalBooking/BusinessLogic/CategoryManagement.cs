﻿using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CategoryManagement : ICategoryManagement
    {
        private ICategoryRepository categoryRepository;

        public CategoryManagement(ICategoryRepository repository)
        {
            categoryRepository = repository;
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                List<Category> allCategories = categoryRepository.GetAll().ToList();
                return allCategories;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotExistCategories, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedAllCategories, e);
            }
        }

        public Category GetById(Guid identifier)
        {
            try
            {
                Category category = categoryRepository.Get(identifier);
                return category;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindCategory, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorGettingCategory, e);
            }
        }

        public List<Category> GetAssociatedCategories(List<Guid> categoriesId)
        {
            List<Category> listOfCategoriesToAssociated = new List<Category>();
            if (categoriesId != null)
            {
                foreach (Guid identifierCategory in categoriesId)
                {
                    Category category = GetById(identifierCategory);
                    listOfCategoriesToAssociated.Add(category);
                }
            }
            return listOfCategoriesToAssociated;
        }
    }
}

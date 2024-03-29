﻿using CardShow.Shared.Constants;
using CardShow.Shared.Enums;
using CardShow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Services
{


    internal static class UrlBuilder
    {
        private static string SetsUrl() => UrlStrings.baseUrl + UrlStrings.sets;
        private static string DeleteSetUrl() => UrlStrings.baseUrl + UrlStrings.sets + "/delete";
        private static string CardsUrl() => UrlStrings.baseUrl + UrlStrings.cards;
        private static string CardsBySetUrl(int setId) => UrlStrings.baseUrl + UrlStrings.cards + $"/{setId}";
        private static string DeleteCardUrl() => UrlStrings.baseUrl + UrlStrings.cards + "/delete";
        private static string AssessmentsUrl() => UrlStrings.baseUrl + UrlStrings.assessments;
        private static string AssessmentsByCardUrl(int cardId) => UrlStrings.baseUrl + UrlStrings.assessments + $"/{cardId}";
        private static string DeleteAssessmentUrl() => UrlStrings.baseUrl + UrlStrings.assessments + "/delete";

        internal static string Build(Type type, APIRequestType reqType)
        {
            if (reqType == APIRequestType.Get)
            {
                if (type == typeof(CardSet))
                    return SetsUrl();
            }

            if (reqType == APIRequestType.Add)
            {
                if (type == typeof(CardSet))
                    return SetsUrl();
                if (type == typeof(Card))
                    return CardsUrl();
                if (type == typeof(Assessment))
                    return AssessmentsUrl();
            }

            return String.Empty;
        }

        internal static string Build(Type type, APIRequestType reqType, int parentId)
        {
            if (reqType == APIRequestType.Get)
            {
                if (type == typeof(Card))
                {
                    return CardsBySetUrl((int)parentId);
                }
            
                if (type == typeof(Assessment))
                {
                    return AssessmentsByCardUrl((int)parentId);
                }
            }

            if (reqType == APIRequestType.Delete)
            {
                if (type == typeof(CardSet))
                    return DeleteSetUrl();
                if (type == typeof(Card))
                    return DeleteCardUrl();
                if (type == typeof(Assessment))
                    return DeleteAssessmentUrl();
            }

            return String.Empty;
        }
    }
}

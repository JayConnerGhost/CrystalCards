using System;
using System.Collections.Generic;
using System.Linq;
using CrystalCards.Api.Dtos;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrystalCards.Api.Controllers
{
    public class CustomControllerBase:ControllerBase
    {
        protected void ProcessLinks(ApplicationDbContext context, IList<LinkRequest> requestLinks, Card entity)
        {
            var linkRequests = ConvertLinkRequests(requestLinks);
            var idLinkToRemove = new List<int>();
            foreach (var link in entity.Links)
            {

                if (linkRequests.FirstOrDefault(x => x.Id == link.Id) == null)
                {
                    idLinkToRemove.Add(link.Id);
                }
            }
            foreach (var id in idLinkToRemove)
            {
                entity.Links.Remove(entity.Links.FirstOrDefault(x => x.Id == id));
            }

         
            foreach (var link in linkRequests)
            {
                var noIdAssignedNewLink = 0;
                //if new point
                if (link.Id == noIdAssignedNewLink)
                {
                    entity.Links.Add(link);
                }
                else
                    //if point edited
                {
                    var linkToUpdate = entity.Links.FirstOrDefault(x => x.Id == link.Id);
                    linkToUpdate.Description = link.Description;
                }
            }
        }

        protected void ProcessActionPoints(ApplicationDbContext context, IList<ActionPointRequest> requestActionPoints, Card entity)
        {
            var actionPoints = ConvertActionPointRequests(requestActionPoints);
            var idPointToRemove = new List<int>();
            foreach (var entryPoints in entity.ActionPoints)
            {

                if (actionPoints.FirstOrDefault(x => x.Id == entryPoints.Id) == null)
                {
                    idPointToRemove.Add(entryPoints.Id);
                }
            }
            foreach (var id in idPointToRemove)
            {
                entity.ActionPoints.Remove(entity.ActionPoints.FirstOrDefault(x => x.Id == id));
            }

            //TODO...
            foreach (var point in actionPoints)
            {
                var noIdAssignedNewPoint = 0;
                //if new point
                if (point.Id == noIdAssignedNewPoint)
                {
                    entity.ActionPoints.Add(point);
                }
                else
                    //if point edited
                {
                    var positiveToUpdate = entity.ActionPoints.FirstOrDefault(x => x.Id == point.Id);
                    positiveToUpdate.Description = point.Description;
                }
            }

        }

        protected void ProcessPoints(ApplicationDbContext context, IList<NPPointRequest> requestNpPoints, Card entry)
        {
            var points = ConvertPointRequests(requestNpPoints);
            //if exists on card but not in points 
            //do find on points if not there then zap on card
            var idPointToRemove = new List<int>();
            foreach (var entryPoints in entry.Points)
            {

                if (points.FirstOrDefault(x => x.Id == entryPoints.Id) == null)
                {
                    idPointToRemove.Add(entryPoints.Id);
                }
            }

            foreach (var id in idPointToRemove)
            {
                entry.Points.Remove(entry.Points.FirstOrDefault(x => x.Id == id));
            }

            foreach (var point in points)
            {
                var noIdAssignedNewPoint = 0;
                //if new point
                if (point.Id == noIdAssignedNewPoint)
                {
                    entry.Points.Add(point);
                }
                else
                    //if point edited
                {
                    var positiveToUpdate = entry.Points.FirstOrDefault(x => x.Id == point.Id);
                    positiveToUpdate.Direction = point.Direction;
                    positiveToUpdate.Description = point.Description;
                    positiveToUpdate.Order = point.Order;
                }
            }
        }

        protected List<ActionPoint> ConvertActionPointRequests(IList<ActionPointRequest> requestActionPoints)
        {
            var actionPoints = new List<ActionPoint>();
            foreach (var request in requestActionPoints)
            {
                actionPoints.Add(new ActionPoint()
                {
                    Id = request.Id,
                    Description = request.Description
                });
            }

            return actionPoints;
        }

        protected List<CardResponse> ConvertResponses(List<Card> result)
        {
            var convertedResponses = new List<CardResponse>();
            foreach (var card in result)
            {
                convertedResponses.Add(ConvertResponse(card));
            }
            return convertedResponses;
        }

        protected IList<NPPointResponse> ConvertPoints(IList<NPPoint> cardPoints)
        {
            var convertedResponses = new List<NPPointResponse>();
            foreach (var point in cardPoints)
            {
                var npPointResponse = new NPPointResponse
                {
                    Description = point.Description,
                    Id = point.Id,
                    Order = point.Order,
                    Direction = point.Direction == 0 ? "Positive" : "Negative"
                };
                convertedResponses.Add(npPointResponse);
            }

            return convertedResponses;
        }

        protected CardResponse ConvertResponse(Card result)
        {
            return new CardResponse()
            {
                Description = result.Description,
                Title = result.Title,
                Id = result.Id,
                Order = result.Order,
                ActionPoints = ConvertActionPoints(result.ActionPoints),
                NPPoints = ConvertPoints(result.Points),
                Links=ConvertLinks(result.Links)

            };
        }

        private IList<LinkResponse> ConvertLinks(IList<Link> resultLinks)
        {
            var responses = new List<LinkResponse>();

            foreach (var link in resultLinks)
            {
                responses.Add(new LinkResponse()
                {
                    Id = link.Id,
                    Description = link.Description,
                    Url = link.Url
                });
            }

            return responses;
        }

        protected IList<ActionPointResponse> ConvertActionPoints(IList<ActionPoint> resultActionPoints)
        {
            var responses = new List<ActionPointResponse>();
            foreach (var actionPoint in resultActionPoints)
            {
                responses.Add(new ActionPointResponse()
                {
                    Id = actionPoint.Id,
                    Description = actionPoint.Description
                });
            }

            return responses;
        }

        private IEnumerable<Link> ConvertLinkRequests(IList<LinkRequest> requestLinks)
        {
            return requestLinks.Select(linkRequest => new Link { Id = linkRequest.Id, Description = linkRequest.Description, Url = linkRequest.Link}).ToList();
        }

        protected IEnumerable<NPPoint> ConvertPointRequests(IList<NPPointRequest> requestNpPoints)
        {
            return requestNpPoints.Select(npPointRequest => new NPPoint { Id = npPointRequest.Id, Direction = Enum.Parse<NPPointDirection>(npPointRequest.Direction), Description = npPointRequest.Description, Order = npPointRequest.Order }).ToList();
        }
    }
}
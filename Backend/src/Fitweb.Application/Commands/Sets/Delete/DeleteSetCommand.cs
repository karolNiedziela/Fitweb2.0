﻿using Fitweb.Application.Requests;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Delete
{
    public class DeleteSetCommand : AuthorizeRequest, IRequest
    {
        [JsonIgnore]
        public int TrainingId { get; set; }

        [JsonIgnore]
        public int ExerciseId { get; set; }

        public int SetId { get; set; }
    }
}

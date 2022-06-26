using System;
using Diary.Model.dto;

namespace Diary.Model.mappers
{
    public class AgendaMapper
    {
        public Agenda FromDto(AgendaDTO agendaDTO)
        {
            Agenda agenda = new Agenda();
            agenda.PrimeiroNome = agendaDTO.PrimeiroNome;
            agenda.UltimoNome = agendaDTO.UltimoNome;
            agenda.Idade = agendaDTO.Idade;
            agenda.Cep = agendaDTO.Cep;
            agenda.Telefone = agendaDTO.Telefone;

            return agenda;
        }

        public AgendaDTO FromModel(Agenda agenda)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.PrimeiroNome = agenda.PrimeiroNome;
            agendaDTO.UltimoNome = agenda.UltimoNome;
            agendaDTO.Idade = agenda.Idade;
            agendaDTO.Cep = agenda.Cep;
            agendaDTO.Telefone = agenda.Telefone;

            return agendaDTO;
        }
    }
}

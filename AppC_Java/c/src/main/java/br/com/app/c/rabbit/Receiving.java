package br.com.app.c.rabbit;

import br.com.app.c.domain.Agenda;
import br.com.app.c.service.AgendaService;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

@Component
public class Receiving {

    private final AgendaService agendaService;

    public Receiving(AgendaService agendaService) {
        this.agendaService = agendaService;
    }

    @RabbitListener(queues = "mensagenss")
    public void receive(String in) throws JsonProcessingException {
        ObjectMapper mapper = new ObjectMapper();
        Agenda agenda = mapper.readValue(in, Agenda.class);

        agendaService.save(agenda);
    }
}

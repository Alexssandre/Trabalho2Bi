package br.com.app.c.controller;

import br.com.app.c.domain.Agenda;
import br.com.app.c.service.AgendaService;
import org.apache.coyote.Response;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/v1/agendas")
public class AgendaController {

    private final AgendaService agendaService;

    public AgendaController(AgendaService agendaService) {
        this.agendaService = agendaService;
    }

    @GetMapping()
    public ResponseEntity<List<Agenda>> getAll(){
        return ResponseEntity.ok(this.agendaService.getAll());
    }
}

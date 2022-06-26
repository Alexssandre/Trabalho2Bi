package br.com.app.c.service;

import br.com.app.c.domain.Agenda;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface AgendaService {

    void save(Agenda entity);

    List<Agenda> getAll();
}

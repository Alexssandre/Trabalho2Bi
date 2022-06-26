package br.com.app.c.service.impl;

import br.com.app.c.domain.Agenda;
import br.com.app.c.repository.AgendaRepository;
import br.com.app.c.service.AgendaService;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class AgendaServiceImpl implements AgendaService {

    private final AgendaRepository repository;

    public AgendaServiceImpl(AgendaRepository repository) {
        this.repository = repository;
    }

    @Override
    public void save(Agenda entity) {
        repository.saveAndFlush(entity);
    }

    @Override
    public List<Agenda> getAll() {
        return repository.findAll();
    }
}

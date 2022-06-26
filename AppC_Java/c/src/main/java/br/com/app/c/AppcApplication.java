package br.com.app.c;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication(scanBasePackages = {"br.com.app.c", "br.com.app.c.*"})
@ComponentScan(basePackages = {"br.com.app.c"})
public class AppcApplication {

	public static void main(String[] args) {
		SpringApplication.run(AppcApplication.class, args);
	}

}

package apiservice.model;

import jakarta.persistence.*;
import lombok.Data;

@Data
@Entity
@Table(name = "bench")
public class Bench {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String name;
}

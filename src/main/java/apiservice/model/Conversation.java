package apiservice.model;

import jakarta.persistence.*;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@Entity
@Table(name = "conversation")
public class Conversation {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private LocalDateTime start;
    private LocalDateTime end;
    private Integer sentiment;
    private String summary;

    @ManyToOne
    @JoinColumn(name="bench_id", nullable=false)
    private Bench bench_;
}
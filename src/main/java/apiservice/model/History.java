package apiservice.model;

import jakarta.persistence.*;
import lombok.Data;

@Data
@Entity
@Table(name = "history")
public class History {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name="bench_id", nullable=false)
    private Bench bench_;

    @ManyToOne
    @JoinColumn(name="location_id", nullable=false)
    private Location location_;

    @ManyToOne
    @JoinColumn(name="status_id", nullable=false)
    private Status status_;
}

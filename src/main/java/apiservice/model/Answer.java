package apiservice.model;

import jakarta.persistence.*;
import lombok.Data;

@Data
@Entity
@Table(name = "answer")
public class Answer {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String response;

    @ManyToOne
    @JoinColumn(name="conversation_id", nullable=false)
    private Conversation conversation_;

    @ManyToOne
    @JoinColumn(name="question_id", nullable=false)
    private Question question_;
}

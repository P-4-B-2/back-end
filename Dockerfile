FROM openjdk:11-jre-slim
COPY empty.jar /app/bike-service.jar
COPY empty.jar /app/customer-service.jar
COPY empty.jar /app/review-service.jar
COPY empty.jar /app/repair-service.jar
COPY api-gateway.jar /app/api-gateway.jar
EXPOSE 8080 8081 8082 8083 8084
CMD java -jar /app/bike-service.jar & \
    java -jar /app/customer-service.jar & \
    java -jar /app/review-service.jar & \
    java -jar /app/repair-service.jar & \
    java -jar /app/api-gateway.jar && \
    tail -f /dev/null

# Фиктивный Dockerfile для Render
FROM alpine:latest
RUN echo "Этот файл нужен только для обхода системы Render"
CMD ["echo", "Используются скрипты build.sh/start.sh вместо Docker"]
# Etap budowania
FROM node:18-alpine AS build

WORKDIR /app

# Kopiujemy package.json i package-lock.json
COPY ./Cwiczenie4_KamilWolak.UI/package*.json ./

# Instalujemy zależności
RUN npm install

# Kopiujemy kod źródłowy
COPY ./Cwiczenie4_KamilWolak.UI/ ./

# Budujemy aplikację
RUN npm run build --prod

# Etap uruchamiania
FROM nginx:alpine

# Usuwamy domyślną stronę index.html
RUN rm -rf /usr/share/nginx/html/*

# Kopiujemy zbudowaną aplikację do Nginx
COPY --from=build /app/dist/cwiczenie4-kamil-wolak.ui /usr/share/nginx/html

# Ustawiamy port
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]

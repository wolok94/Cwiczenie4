# Etap budowania aplikacji Angular
FROM node:18-alpine AS build

# Ustawienie katalogu roboczego
WORKDIR /app

# Kopiowanie plików zależności i ich instalacja
COPY ./Cwiczenie4_KamilWolak.UI/package*.json ./
RUN npm install

# Kopiowanie kodu źródłowego aplikacji
COPY ./Cwiczenie4_KamilWolak.UI/ ./

# Budowanie aplikacji Angular
RUN npm run build --prod

# Etap uruchamiania - Nginx jako serwer
FROM nginx:alpine

# Ustawienie właściciela i uprawnień do katalogu z plikami aplikacji
RUN chown -R nginx:nginx /usr/share/nginx/html && chmod -R 755 /usr/share/nginx/html

# Usunięcie domyślnej strony Nginx
RUN rm -rf /usr/share/nginx/html/*

# Kopiowanie plików aplikacji Angular z etapu build do katalogu Nginx
COPY --from=build /app/dist/cwiczenie4-kamil-wolak.ui/browser /usr/share/nginx/html


# Kopiowanie niestandardowej konfiguracji Nginx (upewnij się, że plik nginx.conf jest w katalogu projektu)
COPY Cwiczenie4_KamilWolak.UI/nginx.conf /etc/nginx/conf.d/default.conf


# Otworzenie portu na serwerze
EXPOSE 80

# Uruchomienie Nginx
CMD ["nginx", "-g", "daemon off;"]

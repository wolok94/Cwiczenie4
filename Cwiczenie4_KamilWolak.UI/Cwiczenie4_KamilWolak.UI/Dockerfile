
FROM node:18-alpine AS build


WORKDIR /app


COPY ./Cwiczenie4_KamilWolak.UI/package*.json ./
RUN npm install


COPY ./Cwiczenie4_KamilWolak.UI/ ./


RUN npm run build --prod


FROM nginx:alpine


RUN chown -R nginx:nginx /usr/share/nginx/html && chmod -R 755 /usr/share/nginx/html


RUN rm -rf /usr/share/nginx/html/*


COPY --from=build /app/dist/cwiczenie4-kamil-wolak.ui/browser /usr/share/nginx/html



COPY Cwiczenie4_KamilWolak.UI/nginx.conf /etc/nginx/conf.d/default.conf



EXPOSE 80


CMD ["nginx", "-g", "daemon off;"]

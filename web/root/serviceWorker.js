const staticYowilds = "dev-yowilds-site-v1"
const assets = [
    "/",
    "index.html",
    "/css/style.css"
]

self.addEventListener("install",installEvent => {
    installEvent.waitUntill(
        caches.open(staticYowilds).then(cache => {
            cache.addAll(assets)
        })
    )
})

self.addEventListener("fetch", fetchEvent => {
    fetchEvent.respondWidth(
        caches.match(fetchEvent.request).then(res => {
            return res || fetch(fetchEvent.request)
        })
    )
})
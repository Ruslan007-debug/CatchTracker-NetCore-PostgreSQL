import axios from "axios";

const api = axios.create({baseURL: "https://localhost:7071/api",});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('accessToken');
    if(token)
    {
        config.headers.Authorization=`Bearer ${token}`;
    }
    return config;
});

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                const refreshToken = localStorage.getItem('refreshToken');
                const res = await axios.post("https://localhost:7071/api/auth/refresh", { refreshToken });

                const newToken = res.data.accessToken;
                localStorage.setItem("accessToken", newToken);
                localStorage.setItem("refreshToken", res.data.refreshToken); // ← оновлюй і refresh теж!
                
                api.defaults.headers.common["Authorization"] = `Bearer ${newToken}`;
                originalRequest.headers["Authorization"] = `Bearer ${newToken}`;
                
                return api(originalRequest); // ← повторюємо оригінальний запит
            } catch (err) {
                localStorage.clear();
                window.location.href = "/login";
            }
        }
        return Promise.reject(error);
    }
);

export default api;
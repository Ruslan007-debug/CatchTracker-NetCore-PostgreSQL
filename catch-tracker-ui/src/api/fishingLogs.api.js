import api from "./axios";

// Отримати мої логи
export const getMyLogs = async () => {
    const response = await api.get('/FishingLogs/userLogs');
    return response.data;
};

// Отримати один лог
export const getLogById = async (id) => {
    const response = await api.get(`/FishingLogs/${id}`);
    return response.data;
};

// Створити новий лог
export const createLog = async (logData) => {
    const response = await api.post('/FishingLogs', logData);
    return response.data;
};

// Оновити лог
export const updateLog = async (id, logData) => {
    const response = await api.put(`/FishingLogs/${id}`, logData);
    return response.data;
};

// Видалити лог
export const deleteLog = async (id) => {
    const response = await api.delete(`/FishingLogs/${id}`);
    return response.data;
};

// ✅ ЛІДЕРБОРД
export const getLeaderboard = async (limit = 50) => {
    const response = await api.get(`/FishingLogs/leaderboard?limit=${limit}`);
    return response.data;
};
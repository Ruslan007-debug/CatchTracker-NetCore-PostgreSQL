import api from "./axios";

export const getAll = async (searchQuery = '') => {
  const response = await api.get('/FishTypes', {
    params: searchQuery ? { typeName: searchQuery } : {},
  });
  return response.data;
};

export const getById = async (id) => {
    const response = await api.get(`/FishTypes/${id}`);
    return response.data;
};

export const create = async (fishTypeData) => {
    const response = await api.post('/FishTypes', fishTypeData);
    return response.data;
};

export const update = async (updateData) => {
    const response = await api.put('/FishTypes', updateData); // або patch
    return response.data;
};

export const deleteType = async (id) => {
    const response = await api.delete(`/FishTypes/${id}`);
    return response.data;
};

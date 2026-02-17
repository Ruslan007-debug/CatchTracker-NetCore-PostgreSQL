import api from "./axios";

export const getAll = async (searchQuery = '') => {
  const response = await api.get('/Places', {
    params: searchQuery ? { name: searchQuery } : {},
  });
  return response.data;
};

export const getById = async (id) => {
    const response = await api.get(`/Places/${id}`);
    return response.data;
};

export const create = async (placeData) => {
    const response = await api.post('/Places', placeData);
    return response.data;
};

export const update = async (updateData) => {
    const response = await api.put('/Places', updateData); // або patch
    return response.data;
};

export const deleteType = async (id) => {
    const response = await api.delete(`/Places/${id}`);
    return response.data;
};

import { useState, useEffect, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { getAll } from "../api/places.api";
import "../CSS/Places.css";

const Places = () => {
    const navigate = useNavigate();
    const [places, setPlaces] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [loading, setLoading] = useState(false);
    
    // 1. Стан для фільтрації по ставках (0 - всі, 1 - перший, 2 - другий)
    const [selectedPond, setSelectedPond] = useState(0);

    const loadPlaces = async (query = '') => {
        setLoading(true);
        try {
            const allPlaces = await getAll(query);
            setPlaces(allPlaces);
        } catch (error) {
            console.error("Помилка завантаження місць:", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(() => {
            loadPlaces(searchQuery);
        }, 500);
        return () => clearTimeout(timer);
    }, [searchQuery]);

    // 2. Логіка фільтрації даних на клієнті
    const filteredPlaces = useMemo(() => {
        if (selectedPond === 0) return places;
        return places.filter(place => place.pondId === selectedPond);
    }, [places, selectedPond]);

    return (
        <div className="places-page">
            <div className="page-header">
                <div className="search-container">
                    <input
                        type="text"
                        className="search-input"
                        placeholder="Пошук по назві місця..."
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                    />
                    <span className="search-icon">🔍</span>
                </div>

                {/* 3. Кнопки перемикання ставків */}
                <div className="pond-filter">
                    <button 
                        className={`pond-btn ${selectedPond === 0 ? "active" : ""}`}
                        onClick={() => setSelectedPond(0)}
                    >
                        Всі ставки
                    </button>
                    <button 
                        className={`pond-btn ${selectedPond === 1 ? "active" : ""}`}
                        onClick={() => setSelectedPond(1)}
                    >
                        Ставок №1
                    </button>
                    <button 
                        className={`pond-btn ${selectedPond === 2 ? "active" : ""}`}
                        onClick={() => setSelectedPond(2)}
                    >
                        Ставок №2
                    </button>
                </div>

                <button className="home-button" onClick={() => navigate("/MainPage")}>
                    На головну
                </button>
            </div>

            <h1 className="page-title">Місця для риболовлі</h1>

            {searchQuery && (
                <p className="search-info">
                    Результати пошуку для: "<strong>{searchQuery}</strong>"
                </p>
            )}

            {loading ? (
                <div className="loading">
                    <p>🎣 Завантаження...</p>
                </div>
            ) : (
                <div className="places-list">
                    {filteredPlaces.length === 0 ? (
                        <div className="no-results">
                            <p>😕 Нічого не знайдено</p>
                        </div>
                    ) : (
                        filteredPlaces.map((place) => (
                            <div key={place.id} className="place-card">
                                <div
                                    className="place-photo"
                                    style={{
                                        backgroundImage: `url(${place.imgUrl || "/placeholder.jpg"})`,
                                    }}
                                >
                                    {/* 4. Бейдж з номером ставка на фото */}
                                    <div className="pond-badge">
                                        Ставок №{place.pondId}
                                    </div>
                                </div>
                                <div className="place-info">
                                    <h3 className="place-name">{place.name}</h3>
                                    <p className="place-detail">
                                        🏆 Трофей: {place.biggestTrophy || "—"}
                                    </p>
                                    <p className="place-detail">
                                        🌡 Температура: {place.waterTemp ?? "—"}°C
                                    </p>
                                    <p className="place-description">
                                        {place.description || "Опис відсутній"}
                                    </p>
                                </div>
                            </div>
                        ))
                    )}
                </div>
            )}

            {!loading && filteredPlaces.length > 0 && (
                <p className="results-count">
                    Знайдено: {filteredPlaces.length} {filteredPlaces.length === 1 ? "місце" : "місць"}
                </p>
            )}
        </div>
    );
};

export default Places;
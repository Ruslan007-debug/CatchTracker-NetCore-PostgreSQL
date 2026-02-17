import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getAll } from "../api/places.api";
import "../CSS/Places.css";


const Places = () =>
    {
        const navigate = useNavigate();
        const [places, setPlaces] = useState([]);
        const [searchQuery, setSearchQuery] = useState('');
        const [loading, setLoading] = useState(false);

        const loadPlaces =async(query = '')=>
            {
                setLoading(true);
                try
                {
                    const allPlaces = await getAll(query)
                    setPlaces(allPlaces);
                }
                catch(error)
                {
                    console.error("Помилка завантаження місць:", error);
                }
                finally
                {
                    setLoading(false);
                }
            };
        
        useEffect(()=>{
            const timer = setTimeout(()=>{
            loadPlaces(searchQuery);}, 500);
            return ()=>clearTimeout(timer); 
        },[searchQuery]);

         return (
            <div className="places-page">
            {/* Шапка */}
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

            {loading && (
                <div className="loading">
                <p>🎣 Завантаження...</p>
                </div>
            )}

            {!loading && (
                <div className="places-list">
                {places.length === 0 ? (
                    <div className="no-results">
                    <p>😕 Нічого не знайдено</p>
                    {searchQuery && (
                        <p>Спробуйте інший запит або очистіть пошук</p>
                    )}
                    </div>
                ) : (
                    places.map((place) => (
                    <div key={place.id} className="place-card">
                        <div
                        className="place-photo"
                        style={{
                            backgroundImage: `url(${place.imgUrl || "/placeholder.jpg"})`,
                        }}
                        ></div>
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

            {!loading && places.length > 0 && (
                <p className="results-count">
                Знайдено: {places.length}{" "}
                {places.length === 1 ? "місце" : "місць"}
                </p>
            )}
            </div>
        );
    };

    export default Places;
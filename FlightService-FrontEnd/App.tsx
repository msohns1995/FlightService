import React from "react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import NavigationBar from "./components/NavigationBar";
import Flight from "./models/flight";
import Ticket from "./models/ticket";
import APIService from "./services/apiService";
import FlightListView from "./views/flights/FlightListView";
import PassengersListView from "./views/passengers/PassengersListView";
import TicketListView from "./views/tickets/TicketListView";
import HomeView from "./views/home/HomeView";


type AppProps = {};

type AppState = {};

class App extends React.Component<AppProps, AppState> {
  constructor(props: AppProps) {
    super(props);
    this.state = {
    }
  }

  componentDidMount() {  }

  render() {
    return (
      <main>
        <NavigationBar />
        <Routes>
          <Route path="/" element={<HomeView />} />

          <Route path="/tickets/*" element={
            <React.Suspense fallback={<>...</>}>
              <TicketListView />
            </React.Suspense>} />

          <Route path="/passengers/*" element={
            <React.Suspense fallback={<>...</>}>
              <PassengersListView />
            </React.Suspense>
          } />

          <Route path="/flights/*" element={
            <React.Suspense fallback={<>...</>}>
              <FlightListView />
            </React.Suspense>
          } />

        </Routes>
      </main>
    );
  }
  componentDidUpdate() { }

  componentDidCatch() { }

  componentWillUnmount() { }
}

export default App;

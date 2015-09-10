// NAnt - A .NET build tool
// Copyright (C) 2001-2008 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Gert Driesen (drieseng@users.sourceforge.net.be)

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;

namespace NAnt.VSNet {
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigurationDictionary : IDictionary, ICollection, IEnumerable {
        #region Private Instance Fields

        private readonly Hashtable _innerHash;

        #endregion Private Instance Fields
        
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDictionary" /> class.
        /// </summary>
        public ConfigurationDictionary() {
            _innerHash = CollectionsUtil.CreateCaseInsensitiveHashtable();
        }

        #endregion Public Instance Constructors

        #region Internal Instance Properties

        internal Hashtable InnerHash {
            get { return _innerHash; }
        }

        #endregion Internal Instance Properties

        #region Implementation of IDictionary

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public ConfigurationDictionaryEnumerator GetEnumerator() {
            return new ConfigurationDictionaryEnumerator(this);
        }
        
        IDictionaryEnumerator IDictionary.GetEnumerator() {
            return GetEnumerator ();
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Remove(Configuration configuration) {
            _innerHash.Remove(configuration);
        }

        void IDictionary.Remove(object key) {
            Remove((Configuration) key);
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contains(Configuration key) {
            return _innerHash.Contains(key);
        }

        bool IDictionary.Contains(object key) {
            return Contains((Configuration) key);
        }

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public void Clear() {
            _innerHash.Clear();      
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(Configuration key, ConfigurationBase value) {
            _innerHash.Add (key, value);
        }

        void IDictionary.Add(object key, object value) {
            Add((Configuration) key, (ConfigurationBase) value);
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return _innerHash.IsReadOnly; }
        }

        /// <summary>
        /// Gets or sets the <see cref="ConfigurationBase"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="ConfigurationBase"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public ConfigurationBase this[Configuration key] {
            get {
                ConfigurationBase foundConfig = (ConfigurationBase) _innerHash[key];
                if (foundConfig == null) {
                    // if no exact match for build configuration and platform
                    // was found, then only match on the name of the configuration
                    //
                    // we need this for two reasons:
                    // 1) when no platform is specified on the <solution> task,
                    //    but we still want to match project configurations with
                    //    a platform.
                    // 2) when a platform is specified on the <solution> task,
                    //    but we want to match any given project configuration
                    //    with the same configuration name.
                    foreach (DictionaryEntry de in _innerHash) {
                        Configuration config = (Configuration) de.Key;
                        if (string.Compare (config.Name, key.Name, true, CultureInfo.InvariantCulture) == 0) {
                            foundConfig = (ConfigurationBase) de.Value;
                            break;
                        }
                    }
                }
                return foundConfig; 
            }
            set { _innerHash[key] = value; }
        }

        object IDictionary.this[object key] {
            get { return this[(Configuration) key]; }
            set { this[(Configuration) key] = (ConfigurationBase) value; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public ICollection Values {
            get { return _innerHash.Values; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public ICollection Keys {
            get { return _innerHash.Keys; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.
        /// </summary>
        public bool IsFixedSize {
            get { return _innerHash.IsFixedSize; }
        }

        #endregion Implementation of IDictionary

        #region Implementation of ICollection

        void ICollection.CopyTo(Array array, int index) {
            _innerHash.CopyTo(array, index);
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized {
            get { return _innerHash.IsSynchronized; }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        public int Count {
            get { return _innerHash.Count; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        public object SyncRoot {
            get { return _innerHash.SyncRoot; }
        }

        #endregion Implementation of ICollection

        #region HashTable Methods

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool ContainsKey (Configuration key) {
            return _innerHash.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the specified value contains value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool ContainsValue(ConfigurationBase value) {
            return _innerHash.ContainsValue(value);
        }

        #endregion HashTable Methods
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationDictionaryEnumerator : IDictionaryEnumerator {
        #region Private Instance Fields

        private readonly IDictionaryEnumerator _innerEnumerator;

        #endregion Private Instance Fields

        #region Internal Instance Constructors

        internal ConfigurationDictionaryEnumerator(ConfigurationDictionary enumerable) {
            _innerEnumerator = enumerable.InnerHash.GetEnumerator();
        }

        #endregion Internal Instance Constructors

        #region Implementation of IDictionaryEnumerator

        /// <summary>
        /// Gets the key of the current dictionary entry.
        /// </summary>
        public Configuration Key {
            get { return (Configuration) _innerEnumerator.Key; }
        }

        object IDictionaryEnumerator.Key {
            get { return Key; }
        }

        /// <summary>
        /// Gets the value of the current dictionary entry.
        /// </summary>
        public ConfigurationBase Value {
            get { return (ConfigurationBase) _innerEnumerator.Value; }
        }

        object IDictionaryEnumerator.Value {
            get { return Value; }
        }

        /// <summary>
        /// Gets both the key and the value of the current dictionary entry.
        /// </summary>
        public DictionaryEntry Entry {
            get { return new DictionaryEntry (Key, Value); }
        }

        #endregion Implementation of IDictionaryEnumerator

        #region Implementation of IEnumerator

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset() {
            _innerEnumerator.Reset();
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext() {
            return _innerEnumerator.MoveNext();
        }

        object IEnumerator.Current {
            get { return Current; }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        public ConfigurationDictionaryEntry Current {
            get { return new ConfigurationDictionaryEntry (Key, Value); }
        }

        #endregion Implementation of IEnumerator
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigurationDictionaryEntry {
        private readonly Configuration _name;
        private readonly ConfigurationBase _config;

        internal ConfigurationDictionaryEntry(Configuration name, ConfigurationBase config) {
            _name = name;
            _config = config;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public Configuration Name {
            get { return _name; }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public ConfigurationBase Config {
            get { return _config; }
        }
    }
}

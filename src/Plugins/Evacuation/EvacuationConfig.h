/*!
 *	@file		EVACUATIONConfig.h
 *	@brief		Configures the shared library aspect of the includes.
 */

#ifndef __EVACUATION_CONFIG_H__
#define __EVACUATION_CONFIG_H__

// Now set up compiler-dependent export/import symbols

#if defined(_MSC_VER)

  #if defined( EVACUATION_STATICLIB )
    #define EVACUATION_API
  #else
    #if defined( EVACUATION_EXPORT )
      // We are building the DLL, export the symbols tagged like this
      #define EVACUATION_API __declspec(dllexport)
    #else
      // If we are consuming the DLL, import the symbols tagged like this
      #define EVACUATION_API __declspec(dllimport)
    #endif
  #endif

#elif defined(__GNUC__)

  #if defined( EVACUATION_STATICLIB )
    #define EVACUATION_API
  #else
    #if defined( EVACUATION_EXPORT )
      #define EVACUATION_API __attribute__ ((visibility ("default")))
    #else
      // If you use -fvisibility=hidden in GCC, exception handling and RTTI
      // would break if visibility wasn't set during export _and_ import
      // because GCC would immediately forget all type infos encountered.
      // See http://gcc.gnu.org/wiki/Visibility
      #define EVACUATION_API __attribute__ ((visibility ("default")))
    #endif
  #endif

#else

  #error Unknown compiler, please implement shared library macros

#endif	// Compiler-detection

#endif // __EVACUATION_CONFIG_H__